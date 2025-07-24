// This file is auto-generated, don't edit it. Thanks.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Darabonba.Utils;
using ESA20240910Client = AlibabaCloud.SDK.ESA20240910.Client;
using AlibabaCloud.OpenApiClient.Models;
using Aliyun.Credentials;
using AlibabaCloud.SDK.ESA20240910.Models;
using AlibabaCloud.TeaUtil;

namespace AlibabaCloud.CodeSample
{
    public class Sample 
    {

        public Sample()
        {
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>初始化账号 Client</para>
        /// </description>
        public static ESA20240910Client CreateESA20240910Client()
        {
            Config config = new Config();
            config.Credential = new Client(null);
            // Endpoint 请参考 https://api.aliyun.com/product/ESA
            config.Endpoint = "esa.cn-hangzhou.aliyuncs.com";
            return new ESA20240910Client(config);
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>调用接口 PurchaseRatePlan 新购套餐</para>
        /// </description>
        public static async Task<PurchaseRatePlanResponseBody> RatePlanInstAsync(ESA20240910Client client)
        {
            Console.WriteLine("[CloudSpec CodeSample] 开始调用接口 PurchaseRatePlan 创建资源");
            PurchaseRatePlanRequest purchaseRatePlanRequest = new PurchaseRatePlanRequest
            {
                Type = "NS",
                ChargeType = "PREPAY",
                AutoRenew = false,
                Period = 1,
                Coverage = "overseas",
                AutoPay = true,
                PlanName = "high",
            };
            PurchaseRatePlanResponse purchaseRatePlanResponse = await client.PurchaseRatePlanAsync(purchaseRatePlanRequest);
            DescribeRatePlanInstanceStatusRequest describeRatePlanInstanceStatusRequest = new DescribeRatePlanInstanceStatusRequest
            {
                InstanceId = purchaseRatePlanResponse.Body.InstanceId,
            };
            int? currentRetry = 0;
            int? delayedTime = 10000;
            int? interval = 10000;

            while (currentRetry < 10) {
                try
                {
                    int? sleepTime = 0;
                    if (currentRetry == 0)
                    {
                        sleepTime = delayedTime;
                    }
                    else
                    {
                        sleepTime = interval;
                    }
                    Console.WriteLine("[CloudSpec CodeSample] 异步检查中");
                    await Task.Delay(sleepTime.Value);
                }
                catch (Darabonba.Exceptions.DaraException error)
                {
                    throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
                    {
                        {"message", error.Message},
                    });
                }
                DescribeRatePlanInstanceStatusResponse describeRatePlanInstanceStatusResponse = await client.DescribeRatePlanInstanceStatusAsync(describeRatePlanInstanceStatusRequest);
                string instanceStatus = describeRatePlanInstanceStatusResponse.Body.InstanceStatus;
                if (instanceStatus == "running")
                {
                    Console.WriteLine("[CloudSpec CodeSample] 调用接口 PurchaseRatePlan 新购套餐 成功, 当前 response 为: ");
                    Console.WriteLine(Common.ToJSONString(purchaseRatePlanResponse));
                    return purchaseRatePlanResponse.Body;
                }
                currentRetry++;
            }
            throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
            {
                {"message", "[CloudSpec CodeSample] 异步检查失败"},
            });
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>调用接口 CreateSite 创建站点</para>
        /// </description>
        public static async Task<CreateSiteResponseBody> SiteAsync(PurchaseRatePlanResponseBody ratePlanInstResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("[CloudSpec CodeSample] 开始调用接口 CreateSite 创建资源");
            CreateSiteRequest createSiteRequest = new CreateSiteRequest
            {
                SiteName = "idltestrecord.com",
                InstanceId = ratePlanInstResponseBody.InstanceId,
                Coverage = "overseas",
                AccessType = "NS",
            };
            CreateSiteResponse createSiteResponse = await client.CreateSiteAsync(createSiteRequest);
            GetSiteRequest getSiteRequest = new GetSiteRequest
            {
                SiteId = createSiteResponse.Body.SiteId,
            };
            int? currentRetry = 0;
            int? delayedTime = 60000;
            int? interval = 10000;

            while (currentRetry < 5) {
                try
                {
                    int? sleepTime = 0;
                    if (currentRetry == 0)
                    {
                        sleepTime = delayedTime;
                    }
                    else
                    {
                        sleepTime = interval;
                    }
                    Console.WriteLine("[CloudSpec CodeSample] 异步检查中");
                    await Task.Delay(sleepTime.Value);
                }
                catch (Darabonba.Exceptions.DaraException error)
                {
                    throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
                    {
                        {"message", error.Message},
                    });
                }
                GetSiteResponse getSiteResponse = await client.GetSiteAsync(getSiteRequest);
                string status = getSiteResponse.Body.SiteModel.Status;
                if (status == "pending")
                {
                    Console.WriteLine("[CloudSpec CodeSample] 调用接口 CreateSite 创建站点 成功, 当前 response 为: ");
                    Console.WriteLine(Common.ToJSONString(createSiteResponse));
                    return createSiteResponse.Body;
                }
                currentRetry++;
            }
            throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
            {
                {"message", "[CloudSpec CodeSample] 异步检查失败"},
            });
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>调用接口 CreateRecord 创建记录</para>
        /// </description>
        public static async Task<CreateRecordResponseBody> RecordCnameAsync(CreateSiteResponseBody siteResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("[CloudSpec CodeSample] 开始调用接口 CreateRecord 创建资源");
            CreateRecordRequest.CreateRecordRequestAuthConf authConf = new CreateRecordRequest.CreateRecordRequestAuthConf
            {
                SecretKey = "hijklmnhij*********mnhijklmn",
                Version = "v4",
                Region = "us-east-1",
                AuthType = "private",
                AccessKey = "abcdefgabcdefgabcdefgabcdefg",
            };
            CreateRecordRequest.CreateRecordRequestData data = new CreateRecordRequest.CreateRecordRequestData
            {
                Value = "www.idltestr.com",
            };
            CreateRecordRequest createRecordRequest = new CreateRecordRequest
            {
                RecordName = "www.idltestrecord.com",
                Comment = "This is a remark",
                Proxied = true,
                SiteId = siteResponseBody.SiteId,
                Type = "CNAME",
                SourceType = "S3",
                Data = data,
                BizName = "api",
                HostPolicy = "follow_hostname",
                Ttl = 100,
                AuthConf = authConf,
            };
            CreateRecordResponse createRecordResponse = await CreateRecordWithRetryAsync(client, createRecordRequest);
            Console.WriteLine("[CloudSpec CodeSample] 调用接口 CreateRecord 创建记录成功, 当前 response: ");
            Console.WriteLine(Common.ToJSONString(createRecordResponse));
            return createRecordResponse.Body;
        }


        public static async Task<CreateRecordResponse> CreateRecordWithRetryAsync(ESA20240910Client client, CreateRecordRequest createRecordRequest)
        {
            string errorCode = "";
            int? currentRetry = 0;
            int? interval = 5000;

            while (currentRetry < 10) {
                try
                {
                    CreateRecordResponse createRecordResponse = await client.CreateRecordAsync(createRecordRequest);
                    Console.WriteLine("[CloudSpec CodeSample] 调用接口 CreateRecord 创建记录 成功, 当前 response 为: ");
                    Console.WriteLine(Common.ToJSONString(createRecordResponse));
                    return createRecordResponse;
                }
                catch (Darabonba.Exceptions.DaraException error)
                {
                    errorCode = error.Code;
                }
                if (errorCode == "Site.ServiceBusy")
                {
                    Console.WriteLine("[CloudSpec CodeSample] 调用接口 CreateRecord 失败, 错误码 Site.ServiceBusy, 重试");
                    await Task.Delay(interval.Value);
                    currentRetry++;
                }
            }
            throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
            {
                {"message", "[CloudSpec CodeSample] 调用接口 CreateRecord 创建记录 失败"},
            });
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>调用接口 UpdateRecord 更新记录</para>
        /// </description>
        public static async Task UpdateRecordCnameAsync(CreateRecordResponseBody createRecordResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("[CloudSpec CodeSample] 开始调用接口 UpdateRecord 更新资源");
            UpdateRecordRequest.UpdateRecordRequestAuthConf authConf = new UpdateRecordRequest.UpdateRecordRequestAuthConf
            {
                SecretKey = "hijklmnhij*********mnhijklmn",
                Version = "v4",
                Region = "us-east-1",
                AuthType = "private",
                AccessKey = "abcdefgabcdefgabcdefgabcdefg",
            };
            UpdateRecordRequest.UpdateRecordRequestData data = new UpdateRecordRequest.UpdateRecordRequestData
            {
                Value = "www.idltestr.com",
            };
            UpdateRecordRequest updateRecordRequest = new UpdateRecordRequest
            {
                Comment = "This is a remark",
                Proxied = true,
                SourceType = "S3",
                Data = data,
                BizName = "web",
                HostPolicy = "follow_hostname",
                Ttl = 100,
                AuthConf = authConf,
                RecordId = createRecordResponseBody.RecordId,
            };
            UpdateRecordResponse updateRecordResponse = await UpdateRecordWithRetryAsync(client, updateRecordRequest);
            Console.WriteLine("[CloudSpec CodeSample] 调用接口 UpdateRecord 更新记录成功, 当前 response: ");
            Console.WriteLine(Common.ToJSONString(updateRecordResponse));
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>调用接口 UpdateRecord 更新记录</para>
        /// </description>
        public static async Task UpdateRecordCname1Async(CreateRecordResponseBody createRecordResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("[CloudSpec CodeSample] 开始调用接口 UpdateRecord 更新资源");
            UpdateRecordRequest.UpdateRecordRequestAuthConf authConf = new UpdateRecordRequest.UpdateRecordRequestAuthConf
            {
                SecretKey = "hijklmnhij*********mnhijklmn",
                Version = "v4",
                Region = "us-east-1",
                AuthType = "private",
                AccessKey = "abcdefgabcdefgabcdefgabcdefg",
            };
            UpdateRecordRequest.UpdateRecordRequestData data = new UpdateRecordRequest.UpdateRecordRequestData
            {
                Value = "www.pangleitestupdate.com",
            };
            UpdateRecordRequest updateRecordRequest = new UpdateRecordRequest
            {
                Comment = "This is a remark",
                Proxied = true,
                SourceType = "S3",
                Data = data,
                BizName = "web",
                HostPolicy = "follow_hostname",
                Ttl = 100,
                AuthConf = authConf,
                RecordId = createRecordResponseBody.RecordId,
            };
            UpdateRecordResponse updateRecordResponse = await UpdateRecordWithRetryAsync(client, updateRecordRequest);
            Console.WriteLine("[CloudSpec CodeSample] 调用接口 UpdateRecord 更新记录成功, 当前 response: ");
            Console.WriteLine(Common.ToJSONString(updateRecordResponse));
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>调用接口 UpdateRecord 更新记录</para>
        /// </description>
        public static async Task UpdateRecordCname2Async(CreateRecordResponseBody createRecordResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("[CloudSpec CodeSample] 开始调用接口 UpdateRecord 更新资源");
            UpdateRecordRequest.UpdateRecordRequestAuthConf authConf = new UpdateRecordRequest.UpdateRecordRequestAuthConf
            {
                SecretKey = "hijklmnhij*********mnhijklmn",
                Version = "v4",
                Region = "us-east-1",
                AuthType = "private",
                AccessKey = "abcdefgabcdefgabcdefgabcdefg",
            };
            UpdateRecordRequest.UpdateRecordRequestData data = new UpdateRecordRequest.UpdateRecordRequestData
            {
                Value = "www.pangleitestupdate.com",
            };
            UpdateRecordRequest updateRecordRequest = new UpdateRecordRequest
            {
                Comment = "This is a remark",
                Proxied = true,
                SourceType = "S3",
                Data = data,
                BizName = "web",
                HostPolicy = "follow_hostname",
                Ttl = 3600,
                AuthConf = authConf,
                RecordId = createRecordResponseBody.RecordId,
            };
            UpdateRecordResponse updateRecordResponse = await UpdateRecordWithRetryAsync(client, updateRecordRequest);
            Console.WriteLine("[CloudSpec CodeSample] 调用接口 UpdateRecord 更新记录成功, 当前 response: ");
            Console.WriteLine(Common.ToJSONString(updateRecordResponse));
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>调用接口 UpdateRecord 更新记录</para>
        /// </description>
        public static async Task UpdateRecordCname3Async(CreateRecordResponseBody createRecordResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("[CloudSpec CodeSample] 开始调用接口 UpdateRecord 更新资源");
            UpdateRecordRequest.UpdateRecordRequestAuthConf authConf = new UpdateRecordRequest.UpdateRecordRequestAuthConf
            {
                SecretKey = "hijklmnhij*********mnhijklmn",
                Version = "v4",
                Region = "us-east-1",
                AuthType = "private",
                AccessKey = "abcdefgabcdefgabcdefgabcdefg",
            };
            UpdateRecordRequest.UpdateRecordRequestData data = new UpdateRecordRequest.UpdateRecordRequestData
            {
                Value = "www.pangleitestupdate.com",
            };
            UpdateRecordRequest updateRecordRequest = new UpdateRecordRequest
            {
                Comment = "DNS记录测试",
                Proxied = true,
                SourceType = "S3",
                Data = data,
                BizName = "web",
                HostPolicy = "follow_hostname",
                Ttl = 3600,
                AuthConf = authConf,
                RecordId = createRecordResponseBody.RecordId,
            };
            UpdateRecordResponse updateRecordResponse = await UpdateRecordWithRetryAsync(client, updateRecordRequest);
            Console.WriteLine("[CloudSpec CodeSample] 调用接口 UpdateRecord 更新记录成功, 当前 response: ");
            Console.WriteLine(Common.ToJSONString(updateRecordResponse));
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>调用接口 UpdateRecord 更新记录</para>
        /// </description>
        public static async Task UpdateRecordCname4Async(CreateRecordResponseBody createRecordResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("[CloudSpec CodeSample] 开始调用接口 UpdateRecord 更新资源");
            UpdateRecordRequest.UpdateRecordRequestAuthConf authConf = new UpdateRecordRequest.UpdateRecordRequestAuthConf
            {
                SecretKey = "secretkey12***********defghijklmn",
                Version = "v2",
                Region = "us-east-2",
                AuthType = "public",
                AccessKey = "AccessKey1234567890abcdefghijklmn",
            };
            UpdateRecordRequest.UpdateRecordRequestData data = new UpdateRecordRequest.UpdateRecordRequestData
            {
                Value = "www.pangleitestupdate.com",
            };
            UpdateRecordRequest updateRecordRequest = new UpdateRecordRequest
            {
                Comment = "DNS记录测试",
                Proxied = true,
                SourceType = "OSS",
                Data = data,
                BizName = "web",
                HostPolicy = "follow_hostname",
                Ttl = 3600,
                AuthConf = authConf,
                RecordId = createRecordResponseBody.RecordId,
            };
            UpdateRecordResponse updateRecordResponse = await UpdateRecordWithRetryAsync(client, updateRecordRequest);
            Console.WriteLine("[CloudSpec CodeSample] 调用接口 UpdateRecord 更新记录成功, 当前 response: ");
            Console.WriteLine(Common.ToJSONString(updateRecordResponse));
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>调用接口 UpdateRecord 更新记录</para>
        /// </description>
        public static async Task UpdateRecordCname5Async(CreateRecordResponseBody createRecordResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("[CloudSpec CodeSample] 开始调用接口 UpdateRecord 更新资源");
            UpdateRecordRequest.UpdateRecordRequestAuthConf authConf = new UpdateRecordRequest.UpdateRecordRequestAuthConf
            {
                SecretKey = "secretkey09**********dcbafedcba",
                Version = "v2",
                Region = "us-gov-west-1",
                AuthType = "private",
                AccessKey = "AccessKey0987654321fedcbafedcba",
            };
            UpdateRecordRequest.UpdateRecordRequestData data = new UpdateRecordRequest.UpdateRecordRequestData
            {
                Value = "www.plexample.com",
            };
            UpdateRecordRequest updateRecordRequest = new UpdateRecordRequest
            {
                Comment = "test_record_comment",
                Proxied = true,
                SourceType = "S3",
                Data = data,
                BizName = "api",
                HostPolicy = "follow_origin_domain",
                Ttl = 86400,
                AuthConf = authConf,
                RecordId = createRecordResponseBody.RecordId,
            };
            UpdateRecordResponse updateRecordResponse = await UpdateRecordWithRetryAsync(client, updateRecordRequest);
            Console.WriteLine("[CloudSpec CodeSample] 调用接口 UpdateRecord 更新记录成功, 当前 response: ");
            Console.WriteLine(Common.ToJSONString(updateRecordResponse));
        }


        public static async Task<UpdateRecordResponse> UpdateRecordWithRetryAsync(ESA20240910Client client, UpdateRecordRequest updateRecordRequest)
        {
            string errorCode = "";
            int? currentRetry = 0;
            int? interval = 3000;

            while (currentRetry < 10) {
                try
                {
                    UpdateRecordResponse updateRecordResponse = await client.UpdateRecordAsync(updateRecordRequest);
                    Console.WriteLine("[CloudSpec CodeSample] 调用接口 UpdateRecord 更新记录 成功, 当前 response 为: ");
                    Console.WriteLine(Common.ToJSONString(updateRecordResponse));
                    return updateRecordResponse;
                }
                catch (Darabonba.Exceptions.DaraException error)
                {
                    errorCode = error.Code;
                }
                if (errorCode == "Record.ServiceBusy")
                {
                    Console.WriteLine("[CloudSpec CodeSample] 调用接口 UpdateRecord 失败, 错误码 Record.ServiceBusy, 重试");
                    await Task.Delay(interval.Value);
                    currentRetry++;
                }
            }
            throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
            {
                {"message", "[CloudSpec CodeSample] 调用接口 UpdateRecord 更新记录 失败"},
            });
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>调用接口 DeleteRecord 删除记录</para>
        /// </description>
        public static async Task DestroyRecordCnameAsync(CreateRecordResponseBody createRecordResponseBody, ESA20240910Client client)
        {
            Console.WriteLine("[CloudSpec CodeSample] 开始调用接口 DeleteRecord 删除资源");
            DeleteRecordRequest deleteRecordRequest = new DeleteRecordRequest
            {
                RecordId = createRecordResponseBody.RecordId,
            };
            DeleteRecordResponse deleteRecordResponse = await DeleteRecordWithRetryAsync(client, deleteRecordRequest);
            Console.WriteLine("[CloudSpec CodeSample] 调用接口 DeleteRecord 删除记录 成功, 当前 response 为: ");
            Console.WriteLine(Common.ToJSONString(deleteRecordResponse));
        }


        public static async Task<DeleteRecordResponse> DeleteRecordWithRetryAsync(ESA20240910Client client, DeleteRecordRequest deleteRecordRequest)
        {
            string errorCode = "";
            int? currentRetry = 0;
            int? interval = 1000;

            while (currentRetry < 10) {
                try
                {
                    DeleteRecordResponse deleteRecordResponse = await client.DeleteRecordAsync(deleteRecordRequest);
                    Console.WriteLine("[CloudSpec CodeSample] 调用接口 DeleteRecord 删除记录 成功, 当前 response 为: ");
                    Console.WriteLine(Common.ToJSONString(deleteRecordResponse));
                    return deleteRecordResponse;
                }
                catch (Darabonba.Exceptions.DaraException error)
                {
                    errorCode = error.Code;
                }
                if (errorCode == "Record.ServiceBusy")
                {
                    Console.WriteLine("[CloudSpec CodeSample] 调用接口 DeleteRecord 失败, 错误码 Record.ServiceBusy, 重试");
                    await Task.Delay(interval.Value);
                    currentRetry++;
                }
            }
            throw new Darabonba.Exceptions.DaraException(new Dictionary<string, string>
            {
                {"message", "[CloudSpec CodeSample] 调用接口 DeleteRecord 删除记录 失败"},
            });
        }


        /// <term><b>Description:</b></term>
        /// <description>
        /// <para>运行代码可能会影响当前账号的线上资源，请务必谨慎操作！</para>
        /// </description>
        public static async Task Main(string[] args)
        {
            // 代码包含涉及到费用的接口，请您确保在使用该接口前，已充分了解收费方式和价格。
            // 设置环境变量 COST_ACK 为 true 或删除下列判断即可运行示例代码
            string costAcknowledged = Environment.GetEnvironmentVariable("COST_ACK");
            if (costAcknowledged.IsNull() || !(costAcknowledged == "true"))
            {
                Console.WriteLine("代码中的 PurchaseRatePlan 接口涉及到费用，请谨慎操作！");
                return ;
            }
            // 初始化 Client
            ESA20240910Client esa20240910Client = CreateESA20240910Client();
            // 初始化资源
            PurchaseRatePlanResponseBody ratePlanInstRespBody = await RatePlanInstAsync(esa20240910Client);
            CreateSiteResponseBody siteRespBody = await SiteAsync(ratePlanInstRespBody, esa20240910Client);
            // 测试Record资源，记录类型为CNAME
            CreateRecordResponseBody recordCnameRespBody = await RecordCnameAsync(siteRespBody, esa20240910Client);
            // 更新资源
            await UpdateRecordCnameAsync(recordCnameRespBody, esa20240910Client);
            await UpdateRecordCname1Async(recordCnameRespBody, esa20240910Client);
            await UpdateRecordCname2Async(recordCnameRespBody, esa20240910Client);
            await UpdateRecordCname3Async(recordCnameRespBody, esa20240910Client);
            await UpdateRecordCname4Async(recordCnameRespBody, esa20240910Client);
            await UpdateRecordCname5Async(recordCnameRespBody, esa20240910Client);
            // 删除资源
            await DestroyRecordCnameAsync(recordCnameRespBody, esa20240910Client);
        }

    }
}

