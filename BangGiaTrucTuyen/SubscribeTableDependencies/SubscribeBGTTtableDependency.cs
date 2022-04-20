using BangGiaTrucTuyen.Hubs;
using BangGiaTrucTuyen.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace BangGiaTrucTuyen.SubscribeTableDependencies
{
    public class SubscribeBGTTtableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<BANGGIATRUCTUYEN> tableDependency;
        DashboardHub dashboardHub;

        public SubscribeBGTTtableDependency(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
        }
        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<BANGGIATRUCTUYEN>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }
        private void TableDependency_OnChanged(object sender, RecordChangedEventArgs<BANGGIATRUCTUYEN> e)
        {
            if(e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                dashboardHub.SendProducts();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(BANGGIATRUCTUYEN)} SqlTableDependency error:{e.Error.Message}");
        }

    }
}
