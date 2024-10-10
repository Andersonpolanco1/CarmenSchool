using CarmenSchool.Core.DTOs.PeriodDTO;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Interfaces.Services;
using CarmenSchool.Core.Models;
using CarmenSchool.Core.Utils;
using System.Linq.Expressions;

namespace CarmenSchool.Services.Internal
{
  internal class PeriodService(IPeriodRepository periodRepository) : IPeriodService
  {
    public async Task<Period> AddAsync(PeriodCreateRequest request)
    {
      await ValidatePeriod(request.GetStartDateAsDateOnly(), request.GetEndDateAsDateOnly());

      var newPeriod = request.ToEntity();
      newPeriod.CreatedDate = DateTime.Now;
      await periodRepository.AddAsync(newPeriod);
      return newPeriod;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
      var periodDb = await periodRepository.GetByIdAsync(id);
      return periodDb != null && await periodRepository.DeleteAsync(periodDb);
    }

    public async Task<IEnumerable<Period>> GetAllAsync()
    {
      var periods = await periodRepository.GetAllAsync();
      return periods is null ?
        [] : periods.OrderByDescending(p => p.StartDate).ToList();
    }

    public async Task<Period?> GetByIdAsync(int id, params Expression<Func<Period, object>>[]? includes)
    {
      var period = await periodRepository.GetByIdAsync(id, includes);
      return period;
    }

    public async Task<bool> UpdateAsync(int id, PeriodUpdateRequest request)
    {
      var period = await periodRepository.GetByIdAsync(id);

      if (period == null)
        return false;

      if (ValidationUtils.FieldHasChanged(request.GetStartDateAsDateOnly(), period.StartDate))
        period.StartDate = request.GetStartDateAsDateOnly()!.Value;

      if (ValidationUtils.FieldHasChanged(request.GetEndDateAsDateOnly(), period.EndDate))
        period.EndDate = request.GetEndDateAsDateOnly()!.Value;

      if (periodRepository.IsModified(period))
      {
        await ValidatePeriod(period.StartDate, period.EndDate);
        return await periodRepository.UpdateAsync(period);
      }

      return true;
    }

    public async Task<IEnumerable<Period>> FindAsync(Expression<Func<Period, bool>> expression)
    {
      var courses = await periodRepository.FindAsync(expression);
      return courses.OrderByDescending(p => p.StartDate);
    }

    public async Task<PaginatedList<Period>> FindAsync(PeriodQueryFilter filters)
    {
      return await periodRepository.FindAsync(filters);
    }

    private async Task ValidatePeriod(DateOnly startDate, DateOnly endDate)
    {
      var period = await periodRepository.FindAsync(p => p.StartDate == startDate && p.EndDate == endDate);

      if (period != null && period.Any())
        throw new InvalidOperationException("Ya existe un curso periodo registrado en la misma fecha de inicio y fin del mismo");

      if (startDate > endDate)
        throw new InvalidOperationException("La fecha de inicio no puede ser posterior a la fecha de fin");
    }
  }
}
