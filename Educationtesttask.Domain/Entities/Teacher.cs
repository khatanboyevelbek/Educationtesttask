﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Educationtesttask.Domain.Enums;

namespace Educationtesttask.Domain.Entities
{
	public class Teacher
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime BirthDate { get; set; }
		public Role Role { get; set; }

		public virtual ICollection<Subject> Subjects { get; set; }

		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdatedDate { get; set; }
	}
}
