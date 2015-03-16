using System;
using System.Configuration;
using System.Diagnostics;
using com.vmware.vcloud.api.rest.schema;
using com.vmware.vcloud.sdk;
using com.vmware.vcloud.sdk.constants.query;
using com.vmware.vcloud.sdk.utility;
using TraceLevel = com.vmware.vcloud.sdk.utility.TraceLevel;

namespace VCloudLib
{
    public class StatusChecker
    {
        public static string GetVmStatus(string vmName)
        {
            try
            {
                var vcloudClient = new vCloudClient(
                    ConfigurationManager.AppSettings["VCUrl"],
                    com.vmware.vcloud.sdk.constants.Version.V5_1);

                vcloudClient.Login(ConfigurationManager.AppSettings["VCUser"], ConfigurationManager.AppSettings["VCUserPwd"]);

                var queryService = vcloudClient.GetQueryService();
                var query = new QueryParams<QueryVMField>
                {
                    Filter = new Filter(new Expression(QueryVMField.NAME, vmName.ToUpper(), ExpressionType.EQUALS))
                };

                RecordResult<QueryResultRecordType> searchResults = queryService.QueryReferences(QueryReferenceType.VM, query).GetRecordResult();
                if (searchResults.GetRecords().Count <= 0) return "NOT_FOUND";
                QueryResultRecordType vmRecord = (searchResults.GetRecords())[0];
                
                var vmstatus = ((QueryResultVMRecordType)(vmRecord)).status;
                Debug.WriteLine(vmstatus);

                return vmstatus;
            }
            catch (VCloudException e)
            {
                Logger.Log(TraceLevel.Critical, e.Message);
                Debug.WriteLine(e.Message);
                return e.Message;
            }

            catch (System.IO.IOException e)
            {
                Logger.Log(TraceLevel.Critical, e.Message);
                Debug.WriteLine(e.Message);
                return e.Message;
            }
            catch (Exception e)
            {
                Logger.Log(TraceLevel.Critical, e.Message);
                Debug.WriteLine(e.Message);
                return e.Message;
            }
        }
    }
}
