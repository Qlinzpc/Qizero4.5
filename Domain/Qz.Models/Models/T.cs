using System;
using System.Collections.Generic;

namespace Qz.Models
{
    public partial class T
    {
        public int RowNumber { get; set; }
        public Nullable<int> EventClass { get; set; }
        public string ApplicationName { get; set; }
        public Nullable<int> ClientProcessID { get; set; }
        public Nullable<int> DatabaseID { get; set; }
        public string DatabaseName { get; set; }
        public Nullable<long> EventSequence { get; set; }
        public Nullable<int> GroupID { get; set; }
        public Nullable<int> Handle { get; set; }
        public string HostName { get; set; }
        public Nullable<int> IsSystem { get; set; }
        public string LoginName { get; set; }
        public byte[] LoginSid { get; set; }
        public string NTDomainName { get; set; }
        public string NTUserName { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<int> SPID { get; set; }
        public string ServerName { get; set; }
        public string SessionLoginName { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<long> TransactionID { get; set; }
        public Nullable<long> XactSequence { get; set; }
        public Nullable<int> CPU { get; set; }
        public Nullable<long> Duration { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> Error { get; set; }
        public Nullable<long> Reads { get; set; }
        public Nullable<long> RowCounts { get; set; }
        public string TextData { get; set; }
        public Nullable<long> Writes { get; set; }
        public Nullable<int> IntegerData { get; set; }
        public Nullable<int> IntegerData2 { get; set; }
        public Nullable<int> LineNumber { get; set; }
        public Nullable<int> NestLevel { get; set; }
        public Nullable<int> Offset { get; set; }
        public Nullable<int> EventSubClass { get; set; }
        public Nullable<int> ObjectID { get; set; }
        public string ObjectName { get; set; }
        public Nullable<int> ObjectType { get; set; }
        public byte[] SqlHandle { get; set; }
        public Nullable<int> State { get; set; }
        public string MethodName { get; set; }
        public byte[] BinaryData { get; set; }
        public Nullable<int> IndexID { get; set; }
        public Nullable<int> Success { get; set; }
        public Nullable<System.Guid> GUID { get; set; }
        public Nullable<int> SourceDatabaseID { get; set; }
        public Nullable<long> BigintData1 { get; set; }
        public Nullable<int> Mode { get; set; }
        public Nullable<long> ObjectID2 { get; set; }
        public Nullable<int> OwnerID { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> Severity { get; set; }
    }
}
