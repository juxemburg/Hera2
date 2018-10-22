using Entities.Valoracion;
using HeraDAL.DataAcess;
using HeraServices.Services.UserServices;
using HeraServices.ViewModels.ApiViewModels;
using HeraServices.ViewModels.ApiViewModels.Exceptions;
using HeraServices.ViewModels.EntitiesViewModels.Chart;
using HeraServices.ViewModels.EntitiesViewModels.Evaluacion;
using HeraServices.ViewModels.EntitiesViewModels.ProfesorCursos;
using HeraServices.ViewModels.EntitiesViewModels.ProfesorEstudiante;
using HeraServices.ViewModels.EntityMapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraServices.ApplicationServices
{
    public class ProfesorCursoService
    {
        private readonly IDataAccess _data;
        private readonly UserService _usrService;

        public ProfesorCursoService(IDataAccess data, UserService usrService)
        {
            _usrService = usrService;
            _data = data;
        }

        public async Task<ApiResult<ProfesorCursoViewModel>> Get_Curso(int profId,
            int cursoId)
        {
            var model = await _data.Find_Curso(cursoId);
            if (model == null || model.ProfesorId != profId)
                throw new ApiNotFoundException("Curso no encontrado curso id: "+model.Id + " ProfesorId:"+ model.ProfesorId + " - "+ profId);

            var registros = await _data.GetAll_RegistroCalificacion(cursoId).ToListAsync();
            return ApiResult<ProfesorCursoViewModel>.Initialize(
                new ProfesorCursoViewModel(
                    model,
                    registros.Where(item => item != null && item.Terminada).ToList(),
                    getChartViewModel(
                       getAggregateTrace(
                        registros
                        .SelectMany(reg => reg.Calificaciones)
                        .Where(cal => cal.ResultadoGeneral != null)
                        .Select(cal => cal.ResultadoGeneral)
                        .Select(res => res.IInfoScratch_General)
                        .ToList()), "Promedio general del curso")), true);
        }

        public async Task<ApiResult<List<RegistroCalificacionViewModel>>> Get_RegistrosEstudiante(int profId,
            int cursoId, int estId)
        {
            if (!(await _data.Exist_Profesor_Curso(profId, cursoId)))
            {
                throw new ApiNotFoundException("Curso no encontrado");
            }

            var registros = await _data.GetAll_RegistroCalificacion(cursoId, estId).ToListAsync();

            return ApiResult<List<RegistroCalificacionViewModel>>.Initialize(registros.Select(item => item.ToViewModel()).ToList(), true);
        }

        private StudentAggregateTraceViewModel getAggregateTrace(List<IInfoScratch_General> data)
        {
            return data
                .Aggregate(
                        seed: new StudentAggregateTraceViewModel
                        {
                            Count = 1,
                            SpriteCountAvg = 0,
                            NonUnusedBlocksAvg = 0,
                            UserDefinedBlocksAvg = 0,
                            CloneUseAvg = 0,
                            SecuenceUseAvg = 0,
                            MultipleThreadsAvg = 0,
                            TwoGreenFlagTrheadAvg = 0,
                            AdvancedEventUseAvg = 0,
                            UseSimpleBlocksAvg = 0,
                            UseMediumBlocksAvg = 0,
                            UseNestedControlAvg = 0,
                            BasicInputUseAvg = 0,
                            VariableUseAvg = 0,
                            SpriteSensingAvg = 0,
                            VariableCreationAvg = 0,
                            BasicOperatorsAvg = 0,
                            MediumOperatorsAvg = 0,
                            AdvancedOperatorsAvg = 0,

                            SpriteCountMode = new List<int>(),
                            NonUnusedBlocksMode = new List<int>(),
                            UserDefinedBlocksMode = new List<int>(),
                            CloneUseMode = new List<int>(),
                            SecuenceUseMode = new List<int>(),
                            MultipleThreadsMode = new List<int>(),
                            TwoGreenFlagTrheadMode = new List<int>(),
                            AdvancedEventUseMode = new List<int>(),
                            UseSimpleBlocksMode = new List<int>(),
                            UseMediumBlocksMode = new List<int>(),
                            UseNestedControlMode = new List<int>(),
                            BasicInputUseMode = new List<int>(),
                            VariableUseMode = new List<int>(),
                            SpriteSensingMode = new List<int>(),
                            VariableCreationMode = new List<int>(),
                            BasicOperatorsMode = new List<int>(),
                            MediumOperatorsMode = new List<int>(),
                            AdvancedOperatorsMode = new List<int>()
                        },
                        func: (acc, item) =>
                        {
                            acc.SpriteCountMode.Add(item.SpriteCount);
                            acc.NonUnusedBlocksMode.Add(item.NonUnusedBlocks);
                            acc.UserDefinedBlocksMode.Add(item.UserDefinedBlocks);
                            acc.CloneUseMode.Add(item.CloneUse);
                            acc.SecuenceUseMode.Add(item.SecuenceUse);
                            acc.MultipleThreadsMode.Add(item.MultipleThreads);
                            acc.TwoGreenFlagTrheadMode.Add(item.TwoGreenFlagTrhead);
                            acc.AdvancedEventUseMode.Add(item.AdvancedEventUse);
                            acc.UseSimpleBlocksMode.Add(item.UseSimpleBlocks);
                            acc.UseMediumBlocksMode.Add(item.UseMediumBlocks);
                            acc.UseNestedControlMode.Add(item.UseNestedControl);
                            acc.BasicInputUseMode.Add(item.BasicInputUse);
                            acc.VariableUseMode.Add(item.VariableUse);
                            acc.SpriteSensingMode.Add(item.SpriteSensing);
                            acc.VariableCreationMode.Add(item.VariableCreation);
                            acc.BasicOperatorsMode.Add(item.BasicOperators);
                            acc.MediumOperatorsMode.Add(item.MediumOperators);
                            acc.AdvancedOperatorsMode.Add(item.AdvancedOperators);

                            return new StudentAggregateTraceViewModel
                            {
                                Count = acc.Count + 1,
                                SpriteCountAvg = (acc.SpriteCountAvg + item.SpriteCount) / acc.Count,
                                NonUnusedBlocksAvg = (acc.NonUnusedBlocksAvg + item.NonUnusedBlocks) / acc.Count,
                                UserDefinedBlocksAvg = (acc.UserDefinedBlocksAvg + item.UserDefinedBlocks) / acc.Count,
                                CloneUseAvg = (acc.CloneUseAvg + item.CloneUse) / acc.Count,
                                SecuenceUseAvg = (acc.SecuenceUseAvg + item.SecuenceUse) / acc.Count,
                                MultipleThreadsAvg = (acc.MultipleThreadsAvg + item.MultipleThreads) / acc.Count,
                                TwoGreenFlagTrheadAvg = (acc.TwoGreenFlagTrheadAvg + item.TwoGreenFlagTrhead) / acc.Count,
                                AdvancedEventUseAvg = (acc.AdvancedEventUseAvg + item.AdvancedEventUse) / acc.Count,
                                UseSimpleBlocksAvg = (acc.UseSimpleBlocksAvg + item.UseSimpleBlocks) / acc.Count,
                                UseMediumBlocksAvg = (acc.UseMediumBlocksAvg + item.UseMediumBlocks) / acc.Count,
                                UseNestedControlAvg = (acc.UseNestedControlAvg + item.UseNestedControl) / acc.Count,
                                BasicInputUseAvg = (acc.BasicInputUseAvg + item.BasicInputUse) / acc.Count,
                                VariableUseAvg = (acc.VariableUseAvg + item.VariableUse) / acc.Count,
                                SpriteSensingAvg = (acc.SpriteSensingAvg + item.SpriteSensing) / acc.Count,
                                VariableCreationAvg = (acc.VariableCreationAvg + item.VariableCreation) / acc.Count,
                                BasicOperatorsAvg = (acc.BasicOperatorsAvg + item.BasicOperators) / acc.Count,
                                MediumOperatorsAvg = (acc.MediumOperatorsAvg + item.MediumOperators) / acc.Count,
                                AdvancedOperatorsAvg = (acc.AdvancedOperatorsAvg + item.AdvancedOperators),
                                SpriteCountMode = acc.SpriteCountMode,
                                NonUnusedBlocksMode = acc.NonUnusedBlocksMode,
                                UserDefinedBlocksMode = acc.UserDefinedBlocksMode,
                                CloneUseMode = acc.CloneUseMode,
                                SecuenceUseMode = acc.SecuenceUseMode,
                                MultipleThreadsMode = acc.MultipleThreadsMode,
                                TwoGreenFlagTrheadMode = acc.TwoGreenFlagTrheadMode,
                                AdvancedEventUseMode = acc.AdvancedEventUseMode,
                                UseSimpleBlocksMode = acc.UseSimpleBlocksMode,
                                UseMediumBlocksMode = acc.UseMediumBlocksMode,
                                UseNestedControlMode = acc.UseNestedControlMode,
                                BasicInputUseMode = acc.BasicInputUseMode,
                                VariableUseMode = acc.VariableUseMode,
                                SpriteSensingMode = acc.SpriteSensingMode,
                                VariableCreationMode = acc.VariableCreationMode,
                                BasicOperatorsMode = acc.BasicOperatorsMode,
                                MediumOperatorsMode = acc.MediumOperatorsMode,
                                AdvancedOperatorsMode = acc.AdvancedOperatorsMode
                            };
                        }); ;
        }

        private ChartMultiLineViewModel getChartViewModel(StudentAggregateTraceViewModel aggregate, string chartName)
        {
            int calcMode(List<int> val)
            {
                return val.GroupBy(item => item)
                       .OrderByDescending(grp => grp.Count())
                       .Select(grp => grp.Key)
                       .FirstOrDefault();
            }

            float calMedian(List<int> val)
            {
                int numberCount = val.Count();
                int halfIndex = (int)Math.Floor((double)(numberCount / 2));
                var sortedNumbers = val.OrderBy(n => n).ToList();
                return sortedNumbers.ElementAtOrDefault(halfIndex);
            }

            return new ChartMultiLineViewModel()
            {
                Name = chartName,
                Labels = new List<string> { "Media", "Mediana", "Moda" },
                AxisLabels = new List<string> {
                        "Número de Sprites",
                        "Se usan todos los bloques",
                        "Use de bloques propios",
                        "Uso de clones",
                        "Uso de secuencias",
                        "Dos hilos por sprite",
                        "Dos hilos inciando con bandera verde",
                        "Más de un tipo de evento",
                        "Uso de bloques de flujo simples",
                        "Uso de bloques complejos",
                        "Uso de bloques anidados",
                        "Uso de bloques de entrada de datos",
                        "Uso de variables no creadas",
                        "Uso de sensores de sprite",
                        "Uso de operadores lógicos básicos",
                        "Uso de operadores lógicos complejos",
                        "Uso de operadores lógicos anidados"
                    },
                Values = new List<List<float>>
                    {
                        new List<float> {
                            aggregate.SpriteCountAvg,
                            aggregate.NonUnusedBlocksAvg,
                            aggregate.UserDefinedBlocksAvg,
                            aggregate.CloneUseAvg,
                            aggregate.SecuenceUseAvg,
                            aggregate.MultipleThreadsAvg,
                            aggregate.TwoGreenFlagTrheadAvg,
                            aggregate.AdvancedEventUseAvg,
                            aggregate.UseSimpleBlocksAvg,
                            aggregate.UseMediumBlocksAvg,
                            aggregate.UseNestedControlAvg,
                            aggregate.BasicInputUseAvg,
                            aggregate.VariableUseAvg,
                            aggregate.SpriteSensingAvg,
                            aggregate.BasicOperatorsAvg,
                            aggregate.MediumOperatorsAvg,
                            aggregate.AdvancedOperatorsAvg
                        },
                        new List<float> {
                            calcMode(aggregate.SpriteCountMode),
                            calcMode(aggregate.NonUnusedBlocksMode),
                            calcMode(aggregate.UserDefinedBlocksMode),
                            calcMode(aggregate.CloneUseMode),
                            calcMode(aggregate.SecuenceUseMode),
                            calcMode(aggregate.MultipleThreadsMode),
                            calcMode(aggregate.TwoGreenFlagTrheadMode),
                            calcMode(aggregate.AdvancedEventUseMode),
                            calcMode(aggregate.UseSimpleBlocksMode),
                            calcMode(aggregate.UseMediumBlocksMode),
                            calcMode(aggregate.UseNestedControlMode),
                            calcMode(aggregate.BasicInputUseMode),
                            calcMode(aggregate.VariableUseMode),
                            calcMode(aggregate.SpriteSensingMode),
                            calcMode(aggregate.BasicOperatorsMode),
                            calcMode(aggregate.MediumOperatorsMode),
                            calcMode(aggregate.AdvancedOperatorsMode)
                            },
                        new List<float> {
                            calMedian(aggregate.SpriteCountMode),
                            calMedian(aggregate.NonUnusedBlocksMode),
                            calMedian(aggregate.UserDefinedBlocksMode),
                            calMedian(aggregate.CloneUseMode),
                            calMedian(aggregate.SecuenceUseMode),
                            calMedian(aggregate.MultipleThreadsMode),
                            calMedian(aggregate.TwoGreenFlagTrheadMode),
                            calMedian(aggregate.AdvancedEventUseMode),
                            calMedian(aggregate.UseSimpleBlocksMode),
                            calMedian(aggregate.UseMediumBlocksMode),
                            calMedian(aggregate.UseNestedControlMode),
                            calMedian(aggregate.BasicInputUseMode),
                            calMedian(aggregate.VariableUseMode),
                            calMedian(aggregate.SpriteSensingMode),
                            calMedian(aggregate.BasicOperatorsMode),
                            calMedian(aggregate.MediumOperatorsMode),
                            calMedian(aggregate.AdvancedOperatorsMode)
                            }
                    }
            };
        }

        public async Task<ApiResult<StudentTracesViewModel>> Get_StudentTraces(int profId, int cursoId, int estId)
        {
            if (!(await _data.Exist_Profesor_Curso(profId, cursoId) && await _data.Exist_Estudiante_Curso(estId, cursoId)))
            {
                throw new ApiNotFoundException("Estudiante no encontrado");
            }

            var estudiante = await _data.Find_Estudiante(estId);
            var registros = _data.GetAll_RegistroCalificacion(cursoId, estId)
                .ToList();

            var studentsTraces = registros
                    .SelectMany(reg => reg.Calificaciones)
                    .Where(cal => cal.ResultadoGeneral != null)
                    .Select(cal => cal.ResultadoGeneral)
                    .Select(res => res.IInfoScratch_General)
                    .ToList();

            var challengesTraces = registros
               .GroupBy(reg => reg.Desafio)
               .Select(grp => new ChallengeTraceViewModel()
               {
                   ChallengeId = grp.Key.Id,
                   ChallengeName = grp.Key.Nombre,
                   ChartModel = getChartViewModel(
                       getAggregateTrace(grp.SelectMany(reg => reg.Calificaciones)
                        .Where(cal => cal.ResultadoGeneral != null)
                        .Select(cal => cal.ResultadoGeneral)
                        .Select(res => res.IInfoScratch_General)
                        .ToList()),
                    "Huellas del desafío")
               });

            var aggregate = getAggregateTrace(studentsTraces);

            return ApiResult<StudentTracesViewModel>.Initialize(new StudentTracesViewModel()
            {
                StudentId = estId,
                StudentName = estudiante.NombreCompleto,
                GeneralTraces = getChartViewModel(aggregate, "Huellas del estudiante"),
                ChallengeTraces = challengesTraces.ToList()
            }, true);
        }

        public async Task<ApiResult<CreateCalificacionCualitativaViewModel>> Do_Calificar(int idProf, int idCurso,
            int idEstudiante, int idDesafio, CreateCalificacionCualitativaViewModel model)
        {

            if (!await _data.Exist_Desafio(idDesafio, idCurso, idProf))
            {
                throw new ApiNotFoundException("Recurso no encontrado");
            }

            var estUserId = (await _usrService
                .Get_EstudianteUserId(idEstudiante)).GetValueOrDefault();
            var desafio = await _data.Find_Desafio(idDesafio);
            var curso = await _data.Find_Curso(idCurso);

            _data.Add(model.Map());
            //Mover
            //_data.Do_PushNotification(
            //    NotificationType.NotificationDesafioCalificado, estUserId,
            //    new Dictionary<string, string>
            //    {
            //        ["IdCurso"] = $"{idCurso}",
            //        ["IdDesafio"] = $"{idDesafio}",
            //        ["NombreDesafio"] = desafio.Nombre,
            //        ["NombreCurso"] = curso.Nombre
            //    });
            var success = await _data.SaveAllAsync();
            return ApiResult<CreateCalificacionCualitativaViewModel>.Initialize(model, success);
        }

        public async Task<ApiResult<CursoDesafioViewModel>> Get_NextChallenge(int teacherId,
            int courseId, int studentId)
        {
            if (!await _data.Exist_Profesor_Curso(teacherId, courseId))
                throw new ApiNotFoundException("Recurso no encontrado");
           
            var curso = await _data.Find_Curso(courseId);
            var rel = await _data.Find_Rel_CursoEstudiantes(courseId, studentId);
            var nextChallenge = await _data.FindPure_Desafio(rel.SiguienteDesafioId);

            if (nextChallenge == null)
                nextChallenge = curso.Desafio;

            return ApiResult<CursoDesafioViewModel>.Initialize(new CursoDesafioViewModel(nextChallenge, nextChallenge.Id == curso.Desafio.Id), true);
        }

        public async Task<ApiResult<bool>> SetNextChallenge(int teacherId, int courseId, int studentId, int challengeId)
        {
            if (!await _data.Exist_Profesor_Curso(teacherId, courseId))
                throw new ApiNotFoundException("Recurso no encontrado");

            if ((await _data.Find_Rel_DesafiosCursos(challengeId, courseId)) == null)
                throw new ApiNotFoundException("El desafío no existe");

            var rel = await _data.Find_Rel_CursoEstudiantes(courseId, studentId);
            rel.SiguienteDesafioId = challengeId;
            _data.Edit(rel);

            return ApiResult<bool>.Initialize(true, await _data.SaveAllAsync());
        }


    }
}
