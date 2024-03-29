﻿using System.Collections.Generic;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface ISurveyAnswersService : IService<SurveyAnswers>
{
    IEnumerable<SurveyAnswers> GetBySurveyQuestionsName(string surveyName);
    IEnumerable<SurveyAnswers> GetAnswersByPatient(long patientId);
}