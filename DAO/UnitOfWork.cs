﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO
{
    using DAO.GenericRepository;
    using DTO;
    using System.Data.Entity.Validation;
    using System.Diagnostics;

    public class UnitOfWork : IDisposable
    {
        private DTO.TourManagerEntities _entityContext = null;

        public UnitOfWork()
        {
            _entityContext = new DTO.TourManagerEntities();
        }


        private GenericRepository<Customer> _customerRepository;
        private GenericRepository<Destination> _destinationRepository;
        private GenericRepository<Employee> _emplyeeRepository;
        private GenericRepository<EmployeeRole> _employeeRoleRepository;
        private GenericRepository<Gender> _genderRepository;
        private GenericRepository<Hotel> _hotelRepository;
        private GenericRepository<Passenger> _passengerRepository;
        private GenericRepository<PassengerCategory> _passengerCategoryRepository;
        private GenericRepository<Status> _statusRepository;
        private GenericRepository<Tour> _tourRepository;
        private GenericRepository<TourCategory> _tourCategoryRepository;
        private GenericRepository<TourGroup> _tourGroupRepository;
        private GenericRepository<TourGroupDetail> _tourGroupDetailRepository;
        private GenericRepository<TourPrice> _tourPriceRepository;
        private GenericRepository<TourSite> _tourSiteRepository;
        private GenericRepository<Transport> _transportRepository;

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new GenericRepository<Customer>(_entityContext);
                return _customerRepository;

            }
        }

        public GenericRepository<Destination> DestinationRepository
        {
            get
            {
                if (_destinationRepository == null)
                    _destinationRepository = new GenericRepository<Destination>(_entityContext);
                return _destinationRepository;
            }
        }

        public GenericRepository<Employee> EmplyeeRepository
        {
            get
            {
                if (_emplyeeRepository == null)
                    _emplyeeRepository = new GenericRepository<Employee>(_entityContext);
                return _emplyeeRepository;
            }
        }

        public GenericRepository<EmployeeRole> EmployeeRoleRepository
        {
            get
            {
                if (_employeeRoleRepository == null)
                    _employeeRoleRepository = new GenericRepository<EmployeeRole>(_entityContext);
                return _employeeRoleRepository;
            }
        }

        public GenericRepository<Gender> GenderRepository
        {
            get
            {
                if (_genderRepository == null)
                    _genderRepository = new GenericRepository<Gender>(_entityContext);
                return _genderRepository;
            }
        }

        public GenericRepository<Hotel> HotelRepository
        {
            get
            {
                if (_hotelRepository == null)
                    _hotelRepository = new GenericRepository<Hotel>(_entityContext);
                return _hotelRepository;
            }
        }

        public GenericRepository<Passenger> PassengerRepository
        {
            get
            {
                if (_passengerRepository == null)
                    _passengerRepository = new GenericRepository<Passenger>(_entityContext);
                return _passengerRepository;
            }
        }

        public GenericRepository<PassengerCategory> PassengerCategoryRepository
        {
            get
            {
                if (_passengerCategoryRepository == null)
                    _passengerCategoryRepository = new GenericRepository<PassengerCategory>(_entityContext);
                return _passengerCategoryRepository;
            }
        }

        public GenericRepository<Status> StatusRepository
        {
            get
            {
                if (_statusRepository == null)
                    _statusRepository = new GenericRepository<Status>(_entityContext);
                return _statusRepository;
            }
        }

        public GenericRepository<Tour> TourRepository
        {
            get
            {
                if (_tourRepository == null)
                    _tourRepository = new GenericRepository<Tour>(_entityContext);
                return _tourRepository;
            }
        }

        public GenericRepository<TourCategory> TourCategoryRepository
        {
            get
            {
                if (_tourCategoryRepository == null)
                    _tourCategoryRepository = new GenericRepository<TourCategory>(_entityContext);
                return _tourCategoryRepository;
            }
        }

        public GenericRepository<TourGroup> TourGroupRepository
        {
            get
            {
                if (_tourGroupRepository == null)
                    _tourGroupRepository = new GenericRepository<TourGroup>(_entityContext);
                return _tourGroupRepository;
            }
        }

        public GenericRepository<TourGroupDetail> TourGroupDetailRepository
        {
            get
            {
                if (_tourGroupDetailRepository == null)
                    _tourGroupDetailRepository = new GenericRepository<TourGroupDetail>(_entityContext);
                return _tourGroupDetailRepository;
            }
        }

        public GenericRepository<TourPrice> TourPriceRepository
        {
            get
            {
                if (_tourPriceRepository == null)
                    _tourPriceRepository = new GenericRepository<TourPrice>(_entityContext);
                return _tourPriceRepository;
            }
        }

        public GenericRepository<TourSite> TourSiteRepository
        {
            get
            {
                if (_tourSiteRepository == null)
                    _tourSiteRepository = new GenericRepository<TourSite>(_entityContext);
                return _tourSiteRepository;
            }
        }

        public GenericRepository<Transport> TransportRepository
        {
            get
            {
                if (_transportRepository == null)
                    _transportRepository = new GenericRepository<Transport>(_entityContext);
                return _transportRepository;
            }
        }



        public void Save()
        {
            try
            {
                _entityContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _entityContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}