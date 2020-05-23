﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    public interface ISubjectRepository
    {
        List<Subject> GetAll();
        Subject GetById(Guid id);
        Subject GetByIdWithChild(Guid id);
        void Delete(Subject subject);
        void AddSubject(Subject subject);
        void SaveChanges();
        void UpdateSubjects(Subject subject);
        List<Subject> GetChildSubjects(Guid id);
    }
}