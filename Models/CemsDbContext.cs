using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CruiseEntertainmentManagnmentSystem.Models
{
    public class CemsDbContext : DbContext
    {
        public CemsDbContext() 
            :base("DefaultConnection")
    {

    }
        public DbSet<Performers> performers{get;set;}
        public DbSet<Groups> groups{get;set;}
        public DbSet<Cabins> cabins{get;set;}
        public DbSet<Cruises> cruises{get;set;}
        public DbSet<CabinCategories> cabincategories{get;set;}
        public DbSet<CabinBooking> CabinBookings { get; set; }
        public DbSet<Position> positions { get; set; }
        public DbSet<BookingType> bookingtype { get; set; }
        public DbSet<CruiseTask> CruiseTasks { get; set; }
        public DbSet<CruiseSchedule> CruiseSchedules { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Shows> shows { get; set; }
        public DbSet<Persons> persons { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<CruiseSubSchedule> CruiseSubSchedules { get; set; }
        public DbSet<PersonMapping> PersonMappings { get; set; }
        public DbSet<DBRole> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<TRF> TRFs { get; set; }
        public DbSet<WNine> WNines { get; set; }
        public DbSet<CruiseNote> Notes { get; set; }
        public DbSet<ContractorShow> ContractorShows { get; set; }
        public DbSet<PersonalInformation> PersonalInformations { get; set; }
        public DbSet<CrewDataForm> CrewdataForms { get; set; }
        public DbSet<PositionMapping> PositionMappings { get; set; }
        public DbSet<PersonalInformationForm> PersonalInformationForms { get; set; }
        public DbSet<ContactList> ContactLists { get; set; }
        public DbSet<FastPayForm> FastPayForms{ get; set; }
        public DbSet<NewVendorForm> NewVendorForms { get; set; }
        
    }
}