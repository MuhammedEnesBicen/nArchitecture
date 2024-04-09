using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentACar.Application.Features.Models.Models;
using RentACar.Application.Services.Repositories;
using RentACar.Domain.Entities;

namespace RentACar.Application.Features.Models.Queries.GetListModel;

public class GetListModelQuery : IRequest<ModelListModel>
{
    public PageRequest PageRequest { get; set; }

    public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, ModelListModel>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public GetListModelQueryHandler(IMapper mapper, IModelRepository modelRepository)
        {
            _mapper = mapper;
            _modelRepository = modelRepository;
        }

        public async Task<ModelListModel> Handle(GetListModelQuery request, CancellationToken cancellationToken)
        {
            //car models
            IPaginate<Model> models = await _modelRepository.GetListAsync(include:
                                            m => m.Include(c => c.Brand),
                                            index: request.PageRequest.Page,
                                            size: request.PageRequest.PageSize);

            //dataModel
            ModelListModel mappedModels = _mapper.Map<ModelListModel>(models);
            return mappedModels;
        }
    }
}
