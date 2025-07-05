using AutoMapper;
using PatientManagement.Data;
using PatientManagement.Models;

namespace PatientManagement.Helper
{
    public class PatientMapper : Profile
    {
        public PatientMapper()
        {
            CreateMap<PatientRequest, Patient>().ReverseMap();
        }
    }
}
