using BackendRubricas.Context;
using BackendReciclarsipaga.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendReciclarsipaga.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly AppDbContext _context;

        public PersonaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            return await _context.persona.ToListAsync();
        }

        public async Task<object> GetWithCiudadAsync(int id)
        {
            return await (
                from persona in _context.persona
                join barrio in _context.barrio on persona.idBarrio equals barrio.idBarrio
                join ciudad in _context.ciudad on barrio.idCiudad equals ciudad.idCiudad
                where persona.idPersona == id
                select new
                {
                    persona.idPersona,
                    persona.documento,
                    persona.idTipoDocumento,
                    persona.primerNombre,
                    persona.segundoNombre,
                    persona.primerApellido,
                    persona.segundoApellido,
                    persona.idBarrio,
                    persona.direccion,
                    persona.telefono,
                    idCiudad = ciudad.idCiudad,
                    descripcionCiudad = ciudad.descripcion
                }).FirstOrDefaultAsync();
        }

        public async Task<object> CreateOrUpdatePersonaAsync(PersonaUsuarioDto dto)
        {
            if (UsuarioExist(dto.Persona.documento))
                return "Usuario ya está registrado en el sistema.";

            if (DocumentoExist(dto.Persona.documento))
            {
                _context.Entry(dto.Persona).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return dto.Persona;
            }
            else
            {
                _context.persona.Add(dto.Persona);
                await _context.SaveChangesAsync();

                int personaId = dto.Persona.idPersona;
                dto.Usuario.idPersona = personaId;
                dto.Usuario.idPerfil = 1;

                _context.usuario.Add(dto.Usuario);
                await _context.SaveChangesAsync();

                return dto;
            }
        }

        public async Task<bool> UpdatePersonaAsync(int id, Persona persona)
        {
            var personaExistente = await _context.persona.FindAsync(id);
            if (personaExistente == null) return false;

            personaExistente.direccion = persona.direccion;
            personaExistente.idBarrio = persona.idBarrio;
            personaExistente.telefono = persona.telefono;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePersonaCompletaAsync(int id, int idPerfil, Persona dto)
        {
            var personaExistente = await _context.persona.FindAsync(id);
            if (personaExistente == null) return false;

            var usuarioExistente = await _context.usuario.FirstOrDefaultAsync(u => u.idPersona == id);
            if (usuarioExistente == null) return false;

            personaExistente.primerNombre = dto.primerNombre;
            personaExistente.segundoNombre = dto.segundoNombre;
            personaExistente.primerApellido = dto.primerApellido;
            personaExistente.segundoApellido = dto.segundoApellido;
            personaExistente.direccion = dto.direccion;
            personaExistente.documento = dto.documento;
            personaExistente.telefono = dto.telefono;
            personaExistente.correo = dto.correo;
            personaExistente.idBarrio = dto.idBarrio;

            usuarioExistente.idPerfil = idPerfil;

            await _context.SaveChangesAsync();
            return true;
        }

        private bool DocumentoExist(int documento)
        {
            return _context.persona.Any(e => e.documento == documento);
        }

        private bool UsuarioExist(int documento)
        {
            return (from u in _context.usuario
                    join p in _context.persona on u.idPersona equals p.idPersona
                    where p.documento == documento
                    select u).Any();
        }
    }
}
