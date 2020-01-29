using System;

namespace TaskList.Domain.Builders
{
    public class TaskBuilder : ITaskBuilder
    {
        private readonly Task _task;

        public TaskBuilder()
        {
            _task = new Task();
        }

        public Task Build()
        {
            return _task;
        }

        public void WithDataDeConclusao(DateTime dataDeConclusao)
        {
            _task.DataDeConclusao = dataDeConclusao;
        }

        public void WithDataDeCriacao(DateTime dataDeCriacao)
        {
            _task.DataDeCriacao = dataDeCriacao;
        }

        public void WithDataDeEdicao(DateTime dataDeEdicao)
        {
            _task.DataDeEdicao = dataDeEdicao;
        }

        public void WithDataDeExclusao(DateTime dataDeExclusao)
        {
            _task.DataDeExclusao = dataDeExclusao;
        }

        public void WithDescricao(string descricao)
        {
            _task.Descricao = descricao;
        }

        public void WithId(Guid id)
        {
            _task.Id = id;
        }

        public void WithStatus(bool status)
        {
            _task.Status = status;
        }

        public void WithTitulo(string titulo)
        {
            _task.Titulo = titulo;
        }
    }
}