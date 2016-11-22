using System.Collections.Generic;

namespace AlexFacultyOfMed.Models
{
    public class Company
    {
        // public virtual ICollection<Company> Companies { get; set; }

        private ICollection<CompanyBranch> _branches;
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompanyBranch> Branches
        {
            get
            {
                _branches = _branches ?? new List<CompanyBranch>();
                return _branches;
            }
            set { _branches = value; }
        }
    }
}