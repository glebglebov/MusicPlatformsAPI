using KMChartsUpdater.BLL.Responses;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface ILabelService
    {
        Response GetLabel(int audioId);

        Response SetLabels(int audioId, string name);

        Response SetLabels(int audioId);

        Response RemoveLabels(int audioId);

        Response SetLabelsForFix(int fixId);

        Response SetLabelsToAll();

        Response RemoveAll();
    }
}
