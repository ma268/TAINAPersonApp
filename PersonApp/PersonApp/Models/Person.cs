using System;
using System.ComponentModel.DataAnnotations;

namespace PersonApp.Models
{
    public class Person
    {
        [Key]
        public long PersonId { get; set; }
        [StringLength(30)]
        public string Firstname { get; set; }
        [StringLength(50)]
        public byte[] Surname { get; set; }
        public Gender Gender { get; set; }
        [StringLength(254)]
        public string EmailAddress { get; set; }
        [StringLength(30)]
        public string PhoneNumber { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }
    }
}
