using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Duende.IdentityServer.Models.IdentityResources;

namespace Infrastructure.Persistence.Configurations
{
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.HasKey(v => v.Id);

            //builder.HasOne(v=>v.)
            //       .WithMany(u => u.AdminPurchaseLogs)
            //       .HasForeignKey();

            builder.Property(v => v.Name)
                    .HasMaxLength(30);
            builder.Property(v=>v.Address)
                    .HasMaxLength (50);
            builder.Property(c => c.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10) // Set maximum length to accommodate any valid phone number length
                    .HasConversion(
                                    phoneNumber => phoneNumber, // Value converter (identity)
                                    phoneNumber => ValidatePhoneNumber(phoneNumber)); // Reverse conversion with validation
            builder.Property(c => c.Email)
                    .IsRequired()
                    .HasMaxLength(100) // Set maximum length for email address
                    .IsUnicode(false) // Ensure email is treated as non-Unicode string
                    .HasConversion(emailAddress => emailAddress.ToLower(), // Convert to lowercase
                               emailAddress => ValidateEmailAddress(emailAddress)); // Custom reverse conversion with validation



        }

        private string ValidateEmailAddress(string emailAddress)
        {
            if (!IsValidEmail(emailAddress))
            {
                throw new ArgumentException("Invalid email address format.");
            }

            // Return the validated email address
            return emailAddress;
        }

        private static bool IsValidEmail(string emailAddress)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(emailAddress);
                return addr.Address == emailAddress;
            }
            catch
            {
                return false;
            }
        }

        private string ValidatePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.StartsWith("0") && phoneNumber.Length != 9)
            {
                throw new ArgumentException("Phone numbers starting with '0' must be 9 digits long.");
            }
            else if (phoneNumber.StartsWith("9") && phoneNumber.Length != 10)
            {
                throw new ArgumentException("Phone numbers starting with '9' must be 10 digits long.");
            }

            // Return the validated phone number
            return phoneNumber;
        }
    }
}
