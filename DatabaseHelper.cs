using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using ProcessMonitorDL.Models;
using System.Linq;

namespace ProcessMonitorDL
{
    public class DatabaseHelper 
    {
        
        private readonly DevHprDevContext _dbContext;

        public DatabaseHelper(DevHprDevContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InsertProcessStatisticsRow(ProcessStatistics procesStatistics)
        {
            try
            {
                Console.WriteLine("Adding a new Process Statistics Row...");

                _dbContext.ProcessStatistics.Add(procesStatistics);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ProcessConfiguration GetProcessConfiguration(string processName)
        {
            ProcessConfiguration processConfiguration = null;
            try
            {
                processConfiguration = _dbContext.ProcessConfigurations.Where(e => e.ProcessName.Equals(processName)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return processConfiguration;
        }

        public int GetPLId(int pcid, int pid, string processName)
        {
            int PLId = 0;
            try
            {
                PLId = _dbContext.ProcessLogs.Where(e => e.Pcid == pcid && e.Pid == pid && e.ProcessName == processName).Select(e => e.Plid).AsEnumerable().DefaultIfEmpty(0).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PLId;
        }


        public void InsertProcessLogRow(ProcessLog processLog)
        {
            try
            {
                _dbContext.ProcessLogs.Add(processLog);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
