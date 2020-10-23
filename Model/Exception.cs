using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Exception
    {
        [Key]
        public int Id { get; set; }
        public string GUID { get; set; }
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
    }
}