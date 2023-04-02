using System;
using System.IO;
using System.Collections.Generic;
using Exiled.API.Features;
using LiteDB;
using VPNShield.Objects;

namespace VPNShield
{
    public static class DbManager
    {
        public static readonly string databaseLocation = Path.Combine(Paths.Exiled, "VPNShield", "Data.db");
        private static readonly LiteDatabase db = new($"Filename={databaseLocation};Connection=shared");

        public static VPNShieldIP GetIP(string IPAddress)
        {
            try
            {
                ILiteCollection<VPNShieldIP> collection = db.GetCollection<VPNShieldIP>("IPAddresses");
                return collection.FindOne(x => x.IPAddress == IPAddress);
            }
            catch(Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }


        public static void SaveIP(VPNShieldIP IPAddress)
        {
            ILiteCollection<VPNShieldIP> collection = db.GetCollection<VPNShieldIP>("IPAddresses");
            collection.Upsert(IPAddress);
        }

        public static void SaveIP(IEnumerable<VPNShieldIP> IPAddresses)
        {
            ILiteCollection<VPNShieldIP> collection = db.GetCollection<VPNShieldIP>("IPAddresses");
            collection.Upsert(IPAddresses);
        }

        public static bool DeleteIP(VPNShieldIP IPAddress) =>
            db.GetCollection<VPNShieldIP>("IPAddresses").Delete(IPAddress.IPAddress);

        public static void ClearIPs() => db.GetCollection<VPNShieldIP>("IPAddresses").DeleteAll();

        public static ILiteCollection<VPNShieldIPSubnet> GetSubnets() => db.GetCollection<VPNShieldIPSubnet>("IPAddressSubnets");

        public static void SaveSubnet(VPNShieldIPSubnet subnet) =>
            db.GetCollection<VPNShieldIPSubnet>("IPAddressSubnets").Upsert(subnet);

        public static bool DeleteSubnet(VPNShieldIPSubnet subnet) =>
            db.GetCollection<VPNShieldIPSubnet>("IPAddressSubnets").Delete(subnet.IPSubnet);

        public static void ClearSubnets() => db.GetCollection<VPNShieldIPSubnet>("IPAddressSubnets").DeleteAll();


        public static VPNShieldUserId GetUserId(string userId)
        {
            try
            {
                ILiteCollection<VPNShieldUserId> collection = db.GetCollection<VPNShieldUserId>("UserIds");
                return collection.FindOne(x => x.UserId == userId);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        //For some reason, Upsert returns true on insert and false on update. Why???
        //You'd expect true = success, false = failure.

        public static void SaveUserId(VPNShieldUserId userId) =>
            db.GetCollection<VPNShieldUserId>("UserIds").Upsert(userId);

        public static void SaveUserId(IEnumerable<VPNShieldUserId> userId) =>
            db.GetCollection<VPNShieldUserId>("UserIds").Upsert(userId);

        public static bool DeleteUserId(VPNShieldUserId userId) =>
            db.GetCollection<VPNShieldUserId>("UserIds").Delete(userId.UserId);

        public static void ClearUserIds() => db.GetCollection<VPNShieldUserId>("UserIds").DeleteAll();
    }
}
