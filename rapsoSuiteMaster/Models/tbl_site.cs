﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace rapsoSuiteMaster.Models
{
    [Table("tbl_site")]
    public partial class tbl_site
    {
        [Key]
        public long id_site { get; set; }
        [Required]
        [StringLength(50)]
        public string site_name { get; set; }
        [Required]
        public string site_description { get; set; }
        public bool site_RapsoSuitesFamily { get; set; }
        [Required]
        [StringLength(150)]
        public string site_nameDeveloper { get; set; }
        [Required]
        [StringLength(450)]
        public string Id_netUser { get; set; }
        public bool site_status { get; set; }

        [ForeignKey("Id_netUser")]
        [InverseProperty("tbl_sites")]
        public virtual AspNetUser Id_netUserNavigation { get; set; }
    }
}