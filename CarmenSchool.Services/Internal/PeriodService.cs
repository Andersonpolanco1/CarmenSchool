using CarmenSchool.Core.DTOs.PeriodDTO;
using CarmenSchool.Core.Interfaces.Repositories;
using CarmenSchool.Core.Interfaces.Services;
using CarmenSchool.Core.Models;
using System.Linq.Expressions;

namespace CarmenSchool.Services.Internal
{
  internal class PeriodService(IPeriodRepository periodRepository) : IPeriodService
  {
    public async Task<PeriodReadDto> AddAsync(PeriodCreateRequest request)
    {
      var period = await periodRepository.FindAsync(p => p.StartDate == request.GetStartDateAsDateOnly() && p.EndDate == request.GetEndDateAsDateOnly());

      if (period != null && period.Any())
        throw new InvalidOperationException("Ya existe un curso periodo registrado en la misma fecha de inicio y fin del mismo");

      if(request.GetStartDateAsDateOnly() > request.GetEndDateAsDateOnly())
        throw new InvalidOperationException("La fecha de inicio no puede ser despues de la fecha fin");

      var newPeriod = request.ToEntity();
      newPeriod.CreatedDate = DateTime.Now;
      await periodRepository.AddAsync(newPeriod);
      return newPeriod.ToRead();
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
      var periodDb = await periodRepository.GetByIdAsync(id);
      return periodDb != null && await periodRepository.DeleteAsync(periodDb);
    }

    public async Task<IEnumerable<PeriodReadDto>> GetAllAsync()
    {
      var periods = await periodRepository.GetAllAsync();
      return periods is null ?
        [] : periods.Select(c => c.ToRead()).OrderByDescending(p => p.StartDate).ToList();
    }

    public async Task<PeriodReadDto?> GetByIdAsync(int id)
    {
      var period = await periodRepository.GetByIdAsync(id);
      return period?.ToRead();
    }

    public async Task<bool> UpdateAsync(int id, PeriodUpdateRequest request)
    {
      var period = await periodRepository.GetByIdAsync(id);

      if (period == null)
        return false;

      if (request.GetStartDateAsDateOnly() != null)
        period.StartDate = request.GetStartDateAsDateOnly()!.Value;

      if (request.EndDate != null)
        period.EndDate = request.GetEndDateAsDateOnly()!.Value;

      return periodRepository.IsModified(period) ?
        await periodRepository.UpdateAsync(period)
        : false;
    }

    public async Task<IEnumerable<PeriodReadDto>> FindAsync(Expression<Func<Period, bool>> expression)
    {
      var courses = await periodRepository.FindAsync(expression);
      return courses.Select(s => s.ToRead()).OrderByDescending(p => p.StartDate);
    }
  }
}
