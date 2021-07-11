using IPStack.Domain.Enums;
using IPStack.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPStack.Domain.Entities
{
    public class Job: IJob
    {
        [Key]
        public Guid Id { get; set; }

        public int Progress { get; set; }

        public int Total { get; set; }

        public JobStatusEnum Status { get; set; }
    }
}
