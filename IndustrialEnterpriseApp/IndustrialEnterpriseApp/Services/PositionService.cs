using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndustrialEnterpriseApp.Data;
using IndustrialEnterpriseApp.Models;

namespace IndustrialEnterpriseApp.Services
{
    public class PositionService
    {
        private readonly IndustrialEnterpriseContext _context;

        public PositionService(IndustrialEnterpriseContext context)
        {
            _context = context;
        }

        public List<Position> GetAllPositions()
        {
            return _context.Positions.ToList();
        }

        public Position GetPositionById(int id)
        {
            return _context.Positions.FirstOrDefault(p => p.Id == id);
        }

        public void AddPosition(Position position)
        {
            _context.Positions.Add(position);
            _context.SaveChanges();
        }

        public void UpdatePosition(Position position)
        {
            _context.Positions.Update(position);
            _context.SaveChanges();
        }

        public void DeletePosition(int id)
        {
            var position = _context.Positions.FirstOrDefault(p => p.Id == id);
            if (position != null)
            {
                _context.Positions.Remove(position);
                _context.SaveChanges();
            }
        }
    }
}
