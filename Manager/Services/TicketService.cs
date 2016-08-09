using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Domain.Models;
using Manager.InfoModels;
using Manager.InputInfoModels;

namespace Manager.Services
{
    public class TicketService
    {
        private readonly ITicketRepository _repository;
        private readonly IMapper _mapper;


        public TicketService(ITicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<TicketInfo> GetAll()
        {
            var tickets = _repository.GetAll();
            var ticketsInfo = _mapper.Map<IEnumerable<TicketInfo>>(tickets);

            return ticketsInfo;
        }

        public OperationResult Create(AddTicketInputInfo ticketInputInfo)
        {
            var newTicket = _mapper.Map<Ticket>(ticketInputInfo);
            _repository.Add(newTicket);

            return new OperationResult(true, Messages.SuccessfullyAddedTicket);
        }



        public OperationResult Update(UpdateTicketInputInfo inputInfo)
        {
            var ticket = _repository.GetById(inputInfo.Id);

            if (ticket == null)
            {
                return new OperationResult(false, Messages.ErrorWhileUpdatingDepartment);
            }

            ticket.FestivalName = inputInfo.FestivalName;
            _repository.Save();

            return new OperationResult(true, Messages.SuccessfullyUpdatedTicket);
        }
    }
}
