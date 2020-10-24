using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Exception
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ApplicationName { get; set; }
        public string MachineName { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string Type { get; set; }
        public bool IsProtected { get; set; } = false;
        public string Host { get; set; }
        public string Url { get; set; }
        public string HTTPMethod { get; set; }
        public string IPAddress { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string StatusCode { get; set; }
        public DateTime DeletionDate { get; set; }
        public string FullJson { get; set; }
        public string ErrorHash { get; set; }
        public int DuplicateCount { get; set; }
        public DateTime LastLogDate { get; set; }
        public string Category { get; set; }
        public string StackTrace { get; set; }
        public string TraceId { get; set; }
        public string Response { get; set; }
    }
}