using IPStack.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPStack.Domain.Interfaces
{
    public interface IJob
    {
        public Guid Id { get; set; }

        public int Progress { get; set; }

        public int Total { get; set; }

        public JobStatusEnum Status { get; set; }
    }
}
