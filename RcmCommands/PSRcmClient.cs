//---------------------------------------------------------------
//  <copyright file="PSRcmClient.cs" company="Microsoft">
//      Copyright (c) Microsoft Corporation. All rights reserved.
//  </copyright>
//
//  <summary>
//  Defines class for PS RCM client.
//  </summary>
//
//  History:     22-Nov-2016    chpadh      Created
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ClientLibHelpers;
using LoggerInterface;
using RcmClientLib;
using RcmContract;

namespace RcmCommands
{
    /// <summary>
    /// Class to define PS RCM client.
    /// </summary>
    public class PSRcmClient
    {
        /// <summary>
        /// Gets or sets credentials used for connecting to RCM Service for inter-service
        /// communication.
        /// </summary>        
        private static RcmCreds RcmClientForInterService { get; set; }

        /// <summary>
        /// Gets or sets credentials used for connecting to RCM Service for on-premises agents.
        /// </summary>        
        private static RcmCreds RcmClientForAgent { get; set; }

        /*
        /// <summary>
        /// Get the test client for agents (Mobility agent, process server, master target).
        /// </summary>
        /// <returns>The RCM client.</returns>
        public static RcmCreds GetRcmClientForAgent()
        {
            // Initialize client library to talk to RCM service.
            string token = RcmClientHelper.GetInterServiceToken(
                "6OSSXwOKM+V11Nj1kyADx5UPiKgAJ21OcGZ2xizCd+A=",
                "http://windowscloudbackup/rc",
                "3698535927408175614",
                "Admin");
            RcmCreds rcmCreds = RcmClientHelper.GetCreds(
                token,
                "https://minint-chpadh1:92");

            return rcmCreds;
        }

        /// <summary>
        /// Get the test client for inter-service communication.
        /// </summary>
        /// <returns>The RCM client.</returns>
        public static RcmCreds GetRcmClientForInterService()
        {
            // Initialize client library to talk to RCM service.
            string token = RcmClientHelper.GetInterServiceToken(
                "6OSSXwOKM+V11Nj1kyADx5UPiKgAJ21OcGZ2xizCd+A=",
                "http://windowscloudbackup/rc",
                "3698535927408175614",
                "Admin");
            RcmCreds rcmCreds = RcmClientHelper.GetCreds(
                token,
                "https://minint-chpadh1:92",
                "702595",
                "8577595522933742722",
                "westus");

            return rcmCreds;
        }
        */

        /// <summary>
        /// Get the test client for agents (Mobility agent, process server, master target).
        /// </summary>
        /// <returns>The RCM client.</returns>
        public static RcmCreds GetRcmClientForAgent()
        {
            string credsFile = 
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + Constants.CredsFile;
            Dictionary<string, object> credsConfig =
                Utilities.GenerateConfigParamsFromFile(credsFile);

            /*
            // Initialize client library to talk to RCM service.
            string token = RcmClientHelper.GetInterServiceToken(
                "6OSSXwOKM+V11Nj1kyADx5UPiKgAJ21OcGZ2xizCd+A=",
                "http://windowscloudbackup/rc",
                "3698535927408175614",
                "Admin");
            RcmCreds rcmCreds = RcmClientHelper.GetCreds(
                token,
                credsConfig["%serviceurl%"].ToString());
            */

            // Set the log context for any tracing to be performed as part of OnStart().
            RcmLogger.Instance.InitializeLogContext(WellKnownActivityId.WebRoleActivityId);

            AadConfiguration aadConfig = new AadConfiguration
            {
                AadAuthorityFormat = "https://login.microsoftonline.com/{0}",
                Audience = "https://DRDataPlaneAppInternal",
                ClientId = "7c07fb08-b554-4955-8baf-13d2f772a65b",
                TenantId = "72f988bf-86f1-41af-91ab-2d7cd011db47",
                CertificateThumbprint = "f352473229786e0c1ec3a9e10035996f62268cfc"
            };

            RcmCreds rcmCreds = RcmClientHelper.GetCreds(
                credsConfig["%serviceurl%"].ToString(),
                aadConfig,
                RcmLogger.Instance);

            return rcmCreds;
        }

        /// <summary>
        /// Get the test client for inter-service communication.
        /// </summary>
        /// <returns>The RCM client.</returns>
        public static RcmCreds GetRcmClientForInterService()
        {
            string credsFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + Constants.CredsFile;
            Dictionary<string, object> credsConfig =
                Utilities.GenerateConfigParamsFromFile(credsFile);

            /*
            // Initialize client library to talk to RCM service.
            string token = RcmClientHelper.GetInterServiceToken(
                "6OSSXwOKM+V11Nj1kyADx5UPiKgAJ21OcGZ2xizCd+A=",
                "http://windowscloudbackup/rc",
                "3698535927408175614",
                "Admin");
            RcmCreds rcmCreds = RcmClientHelper.GetCreds(
                token,
                credsConfig["%serviceurl%"].ToString(),
                credsConfig["%containerid%"].ToString(),
                credsConfig["%resourceid%"].ToString(),
                credsConfig["%resourcelocation%"].ToString());
            */

            // Set the log context for any tracing to be performed as part of OnStart().
            RcmLogger.Instance.InitializeLogContext(WellKnownActivityId.WebRoleActivityId);

            AadConfiguration aadConfig = new AadConfiguration
            {
                AadAuthorityFormat = "https://login.microsoftonline.com/{0}",
                Audience = "https://DRDataPlaneAppInternal",
                ClientId = "7c07fb08-b554-4955-8baf-13d2f772a65b",
                TenantId = "72f988bf-86f1-41af-91ab-2d7cd011db47",
                CertificateThumbprint = "f352473229786e0c1ec3a9e10035996f62268cfc"
            };

            RcmCreds rcmCreds = RcmClientHelper.GetCreds(
                credsConfig["%serviceurl%"].ToString(),
                aadConfig,
                RcmLogger.Instance,
                credsConfig["%containerid%"].ToString(),
                credsConfig["%resourceid%"].ToString(),
                credsConfig["%resourcelocation%"].ToString());

            return rcmCreds;
        }

        /// <summary>
        /// A random value operation context for testing.
        /// This method need to use when the call is not from agent.
        /// </summary>
        /// <returns>A <c>ClientOperationContext</c> object.</returns>
        public static ClientOperationContext GetServiceOperationContext()
        {
            return new ClientOperationContext
            {
                ActivityId = Guid.NewGuid().ToString(),
                ClientRequestId = Guid.NewGuid().ToString(),

                UserHeaders = new Dictionary<string, string>
                {
                    { "x-ms-agent-machine-id", Guid.NewGuid().ToString() },
                    { "x-ms-agent-component-id", RcmEnum.RcmComponentIdentifier.SourceAgent.ToString() }
                }
            };
        }

        /// <summary>
        /// An overloaded method to generate a random value operation context for testing
        /// which accepts input parameters as machine ID and component type.
        /// This method need to be use when the call is from agent.
        /// </summary>
        /// <param name="machineId">Machine ID in question.</param>
        /// <returns>A <c>ClientOperationContext</c> object.</returns>
        public static ClientOperationContext GetAgentOperationContext(string machineId)
        {
            string credsFile =
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + Constants.CredsFile;

            Dictionary<string, object> credsConfig =
                Utilities.GenerateConfigParamsFromFile(credsFile);

            return new ClientOperationContext
            {
                ActivityId = Guid.NewGuid().ToString(),
                ClientRequestId = Guid.NewGuid().ToString(),

                UserHeaders = new Dictionary<string, string>
                {
                    { "x-ms-agent-machine-id", machineId },
                    { "x-ms-agent-component-id", "SourceAgent" },
                    { "x-ms-agent-tenant-id", credsConfig["%tenantid%"].ToString() },
                    { "x-ms-agent-object-id", credsConfig["%objectid%"].ToString() }
                }
            };
        }
    }
}
