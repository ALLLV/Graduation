using HospitalRegistration.DbModel;
using System.Data.Common;

namespace HospitalRegistration.Helpers
{
    public sealed class DbConnectionHelper
    {
        private static DbConnectionHelper instance;
        private static Entities dbContext;
        private static readonly object locker = new object();

        private DbConnectionHelper() { }

        public static DbConnectionHelper Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new DbConnectionHelper();
                        dbContext = new Entities();
                    }

                    return instance;
                }
            }
        }

        public Entities DbContext { get { return dbContext; } }

        public static void InitializeConnection() 
        { 
            if (instance == null)
            {
                instance = new DbConnectionHelper();
                dbContext = new Entities();

                instance.DbContext.Database.Connection.Open();
            }
        }
    }
}
