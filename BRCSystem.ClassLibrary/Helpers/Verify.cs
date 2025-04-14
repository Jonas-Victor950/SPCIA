using BRCSystem.ClassLibrary.Data;
using Microsoft.EntityFrameworkCore;

namespace BRCSystem.ClassLibrary.Helpers
{
    public static class Verify
    {
        public static bool HasCutting(int lotStepDataId, DataContext _context)
        {
            var lotStepData = _context.LotStepDatas.Include(x => x.ProductStep).ThenInclude(x => x.ProcessStep).Single(x => x.Id == lotStepDataId);
            var productLotId = lotStepData.ProductLotId;
            var step = lotStepData.ProductStep.ProcessStep.Step;

            return _context.ProductLots
                    .Include(x => x.LotStepDatas).ThenInclude(x => x.ProductStep).ThenInclude(x => x.ProcessStep).ThenInclude(x => x.Station)
                    .Any(x => x.Id == productLotId && x.LotStepDatas.Any(x => x.ProductStep.ProcessStep.Step <= step && x.ProductStep.ProcessStep.Station.Hascutting));
        }
    }
}