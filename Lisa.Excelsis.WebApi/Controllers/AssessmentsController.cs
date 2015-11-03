//using Lisa.Excelsis.WebApi.Mappers;
//using Lisa.Excelsis.WebApi.Models;
//using Microsoft.AspNet.Mvc;
//using System.Collections.Generic;
//using System.Linq;

//namespace Lisa.Excelsis.WebApi.Controllers
//{
//    [Route("[controller]")]
//    public class AssessmentsController : Controller
//    {
//        // GET: assessment
//        [HttpGet]
//        public IActionResult Get()
//        {
//            var assessments = _db.FetchAssessments();

//            var result = assessments.Select(x => AssessmentMapper.ToTransferObjects(x.Item1, x.Item2));
//            return new HttpOkObjectResult(result);
//        }

//        // GET assessment/{assessmentId}
//        [HttpGet("{id}")]
//        public IActionResult Get(int id)
//        {
//            var assessment = _db.FetchAssessmentById(id);                     

//            var result = AssessmentMapper.ToTransferObject(assessment.Item1, assessment.Item2);
//            return new HttpOkObjectResult(result);
//        }

//        [HttpGet("student/{number}")]
//        public IActionResult Get(string number)
//        {
//            var assessments = _db.FetchAssessmentsByStudent(number);

//            var result = assessments.Select(x => AssessmentMapper.ToTransferObjects(x.Item1, x.Item2));
//            return new HttpOkObjectResult(result);
//        }

//        [HttpPost("/assessments/{subject}/{examName}/{cohort}")]
//        public IActionResult Post([FromBody] AssessmentPost assessmentPost, string subject, string examName, string cohort)
//        {
//            if (!ModelState.IsValid)
//            {
//                var errorList = ModelState.Values.SelectMany(m => m.Errors)
//                                .Select(e => e.Exception.Message)
//                                .ToList();

//                return new BadRequestObjectResult(errorList);
//            }
             
//            var _exam =  _db.FetchExams().Where(e => e.Subject.Name.ToLower() == subject.ToLower()
//                                                  && e.Name.ToLower() == examName.ToLower()
//                                                  && e.Cohort.ToLower() == cohort.ToLower()).FirstOrDefault();

//            if (_exam != null && assessmentPost != null)
//            {
//                Assessment assessment = new Assessment();
//                List<Assessor> assessors = new List<Assessor>();
//                List<Observation> Observations = new List<Observation>();
                               
//                var _student = _db.FetchStudents().Where(s => s.Number.ToLower() == assessmentPost.Student.Number.ToLower()).FirstOrDefault();

//                if (_student == null)
//                {
//                    _student = new Student();
//                    _student.Name = assessmentPost.Student.Name;
//                    _student.Number = assessmentPost.Student.Number;
//                    _db.AddStudent(_student);
//                }
                
//                var _assessors = (from assessorA in _db.FetchAssessors()
//                                  join assessorB in assessmentPost.Assessors
//                                  on assessorA.Username equals assessorB.UserName
//                                  select assessorA);

//                foreach (var assessor in assessmentPost.Assessors)
//                {
//                    if (!_assessors.Any(a => assessor.UserName.Contains(a.Username)))
//                    {
//                        var a = new Assessor
//                        {
//                            Username = assessor.UserName
//                        };
//                        _db.AddAssessor(a);
//                    }
//                }
                
//                assessment.ExamId = _exam.Id;
//                assessment.Student = _student;
//                assessment.Assessed = assessmentPost.Assessed;
//                assessment.Assessors = _assessors.ToList();                
                
//                _db.AddAssessment(assessment);

//                var _criteria = _db.FetchCriteria().Where(c => c.ExamId == _exam.Id);

//                if (_criteria != null)
//                {                   
//                    foreach (var question in _criteria)
//                    {
//                         AssessmentPost.ObservationPost  _observation = assessmentPost.Observations?
//                                                                        .Where(observation => question.Order == observation.CriteriumOrderId).FirstOrDefault();
                        
//                        var o = new Observation();
//                        o.AssessmentId = assessment.Id;
//                        o.Criterium = question;
//                        o.Result = (_observation != null && _observation.Result != null) ? _observation.Result : "";
//                        o.Marks = (_observation != null && _observation.Marks != null) ? string.Join(";", _observation.Marks) : "";
                            
//                        Observations.Add(o);                        
//                    }
//                }

//                _db.AddObservations(Observations);                

//                return new CreatedResult(
//                    "http://localhost:5858/assessments/" + assessment.Id, 
//                    new
//                    {
//                        Id = assessment.Id,
//                        Assessors = assessment.Assessors.Select(a => new
//                        {
//                            Id = a.Id,
//                            Username = a.Username
//                        }),
//                        ExamId = assessment.ExamId,
//                        Assessed = assessment.Assessed,
//                        Student = assessment.Student,
//                        Observations = Observations.Where(o => o.AssessmentId == assessment.Id).Select(o => new                                        
//                        {
//                            Id = o.Id,
//                            Criterium = o.Criterium,
//                            Result = o.Result,
//                            Marks = o.Marks.Split(';')
//                        })
//                    }
//                );
//            }
//            else
//            {
//                return new BadRequestResult();
//            }            
//        }

//        private static string addZero(int DateTime)
//        {
//            return (DateTime < 10) ? "0" + DateTime.ToString() : DateTime.ToString();
//        }

//        private readonly Database _db = new Database();
//    }
//}