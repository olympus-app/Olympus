using System.ComponentModel.DataAnnotations;
using Validator = Olympus.Shared.Utils.Validator;
using Olympus.Server.System;

namespace Olympus.Server.Authentication;

public class User : BaseEntity {

	[MaxLength(64)]
	public string Name { get; protected set; }

	[MaxLength(64)]
	public string Email { get; protected set; }

	[MaxLength(64)]
	public string? Username { get; protected set; }

	[MaxLength(64)]
	public string? Password { get; protected set; }

	[MaxLength(64)]
	public string? JobTitle { get; protected set; }

	[MaxLength(64)]
	public string? Department { get; protected set; }

	[MaxLength(64)]
	public string? OfficeLocation { get; protected set; }

	[MaxLength(64)]
	public string? Country { get; protected set; }

	public byte[]? Photo { get; protected set; }

	public virtual ICollection<UserIdentity>? Identities { get; protected set; }

	private User() {

		Name = null!;
		Email = null!;

	}

	public User(string name, string email) {

		Name = Validator.ValidateName(name);
		Email = Validator.ValidateEmail(email);

	}

	public User(string name, string email, string username, string password) {

		Name = Validator.ValidateName(name);
		Email = Validator.ValidateEmail(email);
		Username = Validator.ValidateString(username);
		Password = Validator.ValidateString(password); // @TODO: implement hashing

	}

	public void UpdateName(string name) {

		Name = Validator.ValidateName(name);

	}

	public void UpdateEmail(string email) {

		Email = Validator.ValidateEmail(email);

	}

	public void UpdateUsername(string username) {

		Username = Validator.ValidateString(username);

	}

	public void UpdatePassword(string password) {

		Password = Validator.ValidateString(password); // @TODO: implement hashing

	}

	public void UpdateJobTitle(string jobTitle) {

		JobTitle = Validator.ValidateString(jobTitle);

	}

	public void UpdateDepartment(string department) {

		Department = Validator.ValidateString(department);

	}

	public void UpdateOfficeLocation(string officeLocation) {

		OfficeLocation = Validator.ValidateString(officeLocation);

	}

	public void UpdateCountry(string country) {

		Country = Validator.ValidateString(country);

	}

	public void UpdatePhoto(byte[]? photo) {

		Photo = photo;

	}

}
