

using BRCSystem.ClassLibrary.Authentication.Entities;
using BRCSystem.ClassLibrary.Authentication.Models;
using BRCSystem.ClassLibrary.Data;
using Microsoft.IdentityModel.Tokens;

namespace BRCSystem.ClassLibrary.Helpers
{

    public interface IParametersService 
    {
        public List<Parameters> GetAll();
        public Parameters? GetById(int id);
        public Parameters Save(ParametersRequest request);
        public void Delete(int id);
        public List<Parameters> GetAllWithPassword();
    }

    public class ParametersService : IParametersService
    {

        private readonly DataContext _context;

        public ParametersService(DataContext context)
        {
            this._context = context;
        }

        public void Delete(int id)
        {
            var model = _context.Parameters.Find(id);
            if (model == null) throw new NoContentException($"Parameter with id {id} not found");
            try
            {
                _context.Parameters.RemoveRange(model);
                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {

            }
        }

        public List<Parameters> GetAll()
        {
            var model = 
            _context.Parameters.Select(p => new Parameters()
                                            {
                                                Id = p.Id,
                                                EmailDomain = p.EmailDomain,
                                                EmailPort = p.EmailPort,
                                                EmailAccount = p.EmailAccount,
                                                AbleMFA = p.AbleMFA
                                            }
                                        ).ToList();
            return model;
        }

        public List<Parameters> GetAllWithPassword()
        {
            var model = 
            _context.Parameters.Select(p => new Parameters()
                                            {
                                                Id = p.Id,
                                                EmailPassword = p.EmailPassword,
                                                EmailDomain = p.EmailDomain,
                                                EmailPort = p.EmailPort,
                                                EmailAccount = p.EmailAccount,
                                                AbleMFA = p.AbleMFA
                                            }
                                        ).ToList();
            return model;
        }

        public Parameters? GetById(int id)
        {
            var model = _context.Parameters.SingleOrDefault(m => m.Id == id);
            return model;
        }


        public Parameters Save(ParametersRequest request)
        {
            Parameters parameters = _context.Parameters.Where(x => x.Id == request.Id).Single();
            
            if (!request.EmailPassword.IsNullOrEmpty())
            {
                parameters.EmailPassword = PasswordEncryption.EncryptPassword(request.EmailPassword);
            }
            
            parameters.EmailDomain = request.EmailDomain;    
            parameters.EmailPort = request.EmailPort;
            parameters.EmailAccount = request.EmailAccount;
            parameters.AbleMFA = request.AbleMFA;
            
            _context.Parameters.UpdateRange(parameters);    
            _context.SaveChanges();
            
            return parameters;
        }
    }
}