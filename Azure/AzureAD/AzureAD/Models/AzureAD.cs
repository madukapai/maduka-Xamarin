using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureAD.Models
{
    public class AzureAD
    {
        public class UserProfile
        {
            public string odatametadata { get; set; }
            public string odatatype { get; set; }
            public string objectType { get; set; }
            public string objectId { get; set; }
            public object deletionTimestamp { get; set; }
            public bool accountEnabled { get; set; }
            public object[] signInNames { get; set; }
            public Assignedlicens[] assignedLicenses { get; set; }
            public Assignedplan[] assignedPlans { get; set; }
            public string city { get; set; }
            public object companyName { get; set; }
            public string country { get; set; }
            public object creationType { get; set; }
            public string department { get; set; }
            public object dirSyncEnabled { get; set; }
            public string displayName { get; set; }
            public object facsimileTelephoneNumber { get; set; }
            public string givenName { get; set; }
            public object immutableId { get; set; }
            public object isCompromised { get; set; }
            public string jobTitle { get; set; }
            public object lastDirSyncTime { get; set; }
            public string mail { get; set; }
            public string mailNickname { get; set; }
            public object mobile { get; set; }
            public object onPremisesSecurityIdentifier { get; set; }
            public object[] otherMails { get; set; }
            public string passwordPolicies { get; set; }
            public object passwordProfile { get; set; }
            public string physicalDeliveryOfficeName { get; set; }
            public string postalCode { get; set; }
            public string preferredLanguage { get; set; }
            public Provisionedplan[] provisionedPlans { get; set; }
            public object[] provisioningErrors { get; set; }
            public string[] proxyAddresses { get; set; }
            public DateTime refreshTokensValidFromDateTime { get; set; }
            public string sipProxyAddress { get; set; }
            public string state { get; set; }
            public string streetAddress { get; set; }
            public string surname { get; set; }
            public string telephoneNumber { get; set; }
            public string usageLocation { get; set; }
            public string userPrincipalName { get; set; }
            public string userType { get; set; }
        }

        public class Assignedlicens
        {
            public object[] disabledPlans { get; set; }
            public string skuId { get; set; }
        }

        public class Assignedplan
        {
            public DateTime assignedTimestamp { get; set; }
            public string capabilityStatus { get; set; }
            public string service { get; set; }
            public string servicePlanId { get; set; }
        }

        public class Provisionedplan
        {
            public string capabilityStatus { get; set; }
            public string provisioningStatus { get; set; }
            public string service { get; set; }
        }
    }
}
