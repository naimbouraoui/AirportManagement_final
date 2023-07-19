using AM.Core.Domain;
using AM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Core.Service
{
    public class PlaneService : Service<Plane>, IPlaneService
    {
        //IRepository<Plane> _planeRepository;
        //readonly IUnitOfWork _unitOfWork;
        //readonly IService<Flight> flightService;
        readonly IFlightService flightService;
        public PlaneService(IUnitOfWork _unitOfWork, IFlightService flightService) : base(_unitOfWork)
        {
            this.flightService = flightService;
            //this._unitOfWork = _unitOfWork;
            //_planeRepository = _unitOfWork.GetRepository<Plane>();

        }

        public IList<Passenger> GetPassengers(Plane pl)
        {
            throw new NotImplementedException();
        }
        //public void Add(Plane p)
        //{
        //    _planeRepository.Add(p);
        //    _unitOfWork.Save();
        //}

        //public void Remove(Plane p)
        //{
        //    _planeRepository.Delete(p.PlaneId);
        //    _unitOfWork.Save();
        //}

        //public IList<Plane> GetAll()
        //{
        //    return _planeRepository.GetAll();

        //}
       /* public IList<Passenger> GetPassengers(Plane plane)
        {
            return Get(plane.PlaneId)
                .flights.SelectMany(f => f.Reservations)
                .Select(r => r.MyPassenger).Distinct().ToList();
        }*/
        public IList<Flight> GetFlights(int nbrPlane)
        {
            return GetAll().OrderByDescending(p => p.ManufactureDate).Take(nbrPlane)
                .SelectMany(propa => propa.flights).OrderByDescending(f => f.FlightDate).ToList();
        }
        public bool IsAvailable(int n, Flight flight)
        {
            var flightDb = flightService.Get(flight.FlightId);
            return flightDb.MyPlane.Capacity - flightDb.Reservations.Count >= n;
        }
        public void DeleteUselessPlanes()
        {
             GetAll().Where(p => p.flights.Where(f => f.FlightDate.AddYears(1) >= DateTime.Now).Count() == 0).ToList().
             ForEach(p => Remove(p.PlaneId));
        }
    }
}























