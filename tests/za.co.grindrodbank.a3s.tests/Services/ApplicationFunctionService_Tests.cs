/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using za.co.grindrodbank.a3s.MappingProfiles;
using za.co.grindrodbank.a3s.Models;
using za.co.grindrodbank.a3s.Repositories;
using za.co.grindrodbank.a3s.Services;
using AutoMapper;
using NSubstitute;
using Xunit;

namespace za.co.grindrodbank.a3s.tests.Services
{
    public class ApplicationFunctionService_Tests
    {
        IMapper mapper;
        Guid userGuid;
        Guid applicationFunctionGuid;

        ApplicationFunctionModel mockedApplicationFunction;

        public ApplicationFunctionService_Tests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationFunctionResourceApplicationFunctionModelProfile());
                cfg.AddProfile(new UserResourceUserModelProfile());
            });

            mapper = config.CreateMapper();
            applicationFunctionGuid = Guid.NewGuid();
            userGuid = Guid.NewGuid();

            mockedApplicationFunction = new ApplicationFunctionModel();
            mockedApplicationFunction.Name = "Test applicationFunction";
            mockedApplicationFunction.Id = applicationFunctionGuid;
        }

        [Fact]
        public async Task GetById_GivenGuid_ReturnsApplicationFunctionResource()
        {
            var userRepository = Substitute.For<IUserRepository>();
            var applicationFunctionRepository = Substitute.For<IApplicationFunctionRepository>();

            applicationFunctionRepository.GetByIdAsync(applicationFunctionGuid).Returns(mockedApplicationFunction);

            var applicationFunctionService = new ApplicationFunctionService(applicationFunctionRepository, mapper);
            var applicationFunctionResource = await applicationFunctionService.GetByIdAsync(applicationFunctionGuid);

            Assert.True(applicationFunctionResource.Name == "Test applicationFunction", $"Expected applicationFunction name: '{applicationFunctionResource.Name}' does not equal expected value: 'Test applicationFunction'");
            Assert.True(applicationFunctionResource.Uuid == applicationFunctionGuid, $"Expected applicationFunction UUID: '{applicationFunctionResource.Uuid}' does not equal expected value: '{applicationFunctionGuid}'");
        }
    }
}
