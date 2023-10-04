import React, { Component } from 'react';
import { format } from 'date-fns';
import api from '.././api';

export class Tasks extends Component {
    static displayName = Tasks.name;

    constructor(props) {
        super(props);
        var token = localStorage.getItem("token");
        this.state = {
            statusList: [
                { id: 0, title: "Pendente", class: "default" },
                { id: 1, title: "Em progresso", class: "primary" },
                { id: 2, title: "Completo", class: "success" }
            ],
            statusFilter: -1,
            taskList: [],
            title: "",
            description: "",
            status: 0,
            config: {
                headers: {
                    Accept: "application/json",
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`
                }
            },
            user: JSON.parse(localStorage.getItem('user'))
        };
        this.getTasks = this.getTasks.bind(this);
        this.createTask = this.createTask.bind(this);
        this.deleteTask = this.deleteTask.bind(this);
    }

    componentDidMount() {
        this.getTasks();
    }

    getTasks() {
        api.get('/api/tasks', this.state.config)
            .then(x => {
                this.setState({ taskList: x.data })
            });
    }


    filterByStatus(id) {
        this.setState({ statusFilter: id })
        if (id == -1)
            return this.getTasks();

        api.get('/api/tasks/status/' + Number(id), this.state.config)
            .then(x => {
                this.setState({ taskList: x.data})
            });
    }

    createTask(e) {
        e.preventDefault();

        var task = {
            Title: this.state.title,
            Description: this.state.description,
            Status: this.state.status
        };
        
        api.post('/api/tasks', task, this.state.config)
            .then(x => {
                this.getTasks();
                this.setState({
                    title: "",
                    description: "",
                    status: 0,
                    defaultStatusFilter: -1
                });
            });
    }

    deleteTask(id, e) {
        e.preventDefault();
        api.delete('/api/tasks/' + id, this.state.config)
            .then(x => {
                this.getTasks();
                this.setState({
                    title: "",
                    description: "",
                    status: 0
                });
            });
    }

    updateTask(id, e) {
        e.preventDefault();
        const { taskList } = this.state;
        var task = taskList.find(x => x.id === id);
        api.put('/api/tasks/' + id, task, this.state.config)
            .then(x => {
                this.getTasks();
                this.setState({
                    title: "",
                    description: "",
                    status: 0,
                   
                });
            });
    }

    handleTaskTitleChange(id, title) {
        const { taskList } = this.state;
        var task = taskList.find(x => x.id === id);
        task.title = title;
        this.setState({ taskList });
    }


    handleTaskDescriptionChange(id, description) {
        const { taskList } = this.state;
        var task = taskList.find(x => x.id === id);
        task.description = description;
        this.setState({ taskList });
    }

    handleTaskStatusChange(id, status) {
        const { taskList } = this.state;
        var task = taskList.find(x => x.id === id);
        task.status = Number(status);
        this.setState({ taskList });
    }

    render() {
        const { taskList } = this.state;
        return (
            <div>
                <div className="row">
                    <h1>Criar Tarefa</h1>
                    <form onSubmit={e => this.createTask(e)} className="col-md-6">
                        <div className="mb-3">
                            <label htmlFor="title" className="form-label">Título</label>
                            <input required value={this.state.title} onChange={(e) => this.setState({ title: e.target.value })} type="text" className="form-control" id="title" placeholder="Digite o título" />
                        </div>
                        <div className="mb-3">
                            <label htmlFor="description" className="form-label">Descrição</label>
                            <textarea required value={this.state.description} onChange={(e) => this.setState({ description: e.target.value })} className="form-control" id="description" placeholder="Digite a descrição" />
                        </div>
                        <button type="submit" className="btn btn-primary">Criar</button>
                    </form>
                </div>
                <br />
                <div className="row">
                    <div className="col-md-12">
                        <h1>Lista de Tarefas</h1>
                        <p>Filtrar por Status: </p>
                        <select autoComplete='chrome-off' className="form-control" value={this.state.statusFilter} onChange={(e) => this.filterByStatus(e.target.value)}>
                            <option value={-1}>Todos</option>
                            {this.state.statusList.map(x => <option key={'status-filter-' + x.id} value={x.id}>{x.title}</option>)}
                        </select>
                        <br />
                    </div>
                    {taskList.map((task) => {
                        var status = this.state.statusList.find(x => x.id == task.status);
                        return <div key={task.id} className="col-md-3">
                            <div className="card mb-3">
                                <div className="card-header">
                                    {this.state.user.id == task.userId ?
                                        <>
                                            <input autoComplete='chrome-off' value={task.title} onChange={(e) => this.handleTaskTitleChange(task.id, e.target.value)} className="form-control" ></input>
                                            <br />
                                            <select className="form-control" defaultValue={task.status} onChange={(e) => this.handleTaskStatusChange(task.id, e.target.value)}>
                                                {this.state.statusList.map(x => <option key={'status-' + x.id} value={x.id}>{x.title}</option>)}
                                            </select>
                                        </>
                                        :
                                        <> <h5 className="card-title">{task.title}</h5><span className={"badge bg-" + status.class}>{status.title}</span></>
                                    }
                                </div>
                                <div className="card-body">
                                    {this.state.user.id == task.userId ? <textarea autoComplete='chrome-off' value={task.description} onChange={(e) => this.handleTaskDescriptionChange(task.id, e.target.value)} className="form-control" ></textarea> : <p className="card-text">{task.description}</p>}
                                </div>
                                <div className="card-footer">
                                    <p> {task.userName} ({format(new Date(task.createdAt), 'dd/MM/yyyy HH:mm:ss')})</p>
                                    {this.state.user.id == task.userId && <div >
                                        <form className="col-md-3">
                                            <div className="d-flex justify-content-between align-items-center">
                                                <button className="btn btn-success" onClick={(e) => this.updateTask(task.id, e)}>Alterar</button>
                                                <button className="btn btn-danger" onClick={(e) => this.deleteTask(task.id, e)}>Deletar</button>
                                            </div>
                                        </form>
                                    </div>}
                                </div>
                            </div>
                        </div>

                    })}
                </div>

            </div>

        );
    }
}
