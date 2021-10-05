using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Responses;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface IChartService
    {
        void UpdateChart(int chartId);

        GetChartFixResponse GetChart(int id, int offset, int limit, string date = null);

        ChartFixDto GetAllFixes(int id);

        void DeleteFix(int id);

        void DeleteChart(int id);

        void Date(int id, int day, int month, int year);
    }
}
