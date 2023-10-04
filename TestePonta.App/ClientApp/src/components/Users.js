import React, { Component, useState } from 'react';
import api from '.././api';

export class Users extends Component {

    static displayName = Users.name;

    constructor(props) {
        super(props);
        this.state = {
            userList: [],
            userName: "",
            email: "",
            password: "",
            error: ""
        };
        this.createUser = this.createUser.bind(this);
    }


    componentDidMount() {
        this.getUsers();
        
    }

    getUsers() {
        api.get('/api/users', {})
            .then(x => {
                this.setState({ userList: x.data })
            });
    }

    createUser(e) {
        e.preventDefault();

        this.setState({ error: "" });
        var user = {
            Name: this.state.userName,
            Email: this.state.email,
            Password: this.state.password
        };

        api.post('/api/users', user)
            .then(x => {
                this.getUsers();
                this.setState({
                    userName: "",
                    email: "",
                    password: ""
                });
            }).catch(x => {
                this.setState({ error: x.response.data });
            });;
    }

    deleteUser(id, e) {
        e.preventDefault();
        api.delete('/api/users/' + id, this.state.config)
            .then(x => {
                this.getUsers();
                this.setState({
                    userName: "",
                    email: "",
                    password: ""
                });
            });
    }

    render() {
        const { userList } = this.state;
        return (
            <div>
                <div className="row">
                    <h1>Criar Usuário</h1>
                    <form autoComplete="new-password" onSubmit={e => this.createUser(e)} className="col-md-6">
                        <div className="mb-3">
                            <label htmlFor="name" className="form-label" >Nome</label>
                            <input required autoComplete='chrome-off' value={this.state.userName} onChange={(e) => this.setState({ userName: e.target.value })} type="text" className="form-control" id="name" placeholder="Digite o nome" />
                        </div>
                        <div className="mb-3">
                            <label htmlFor="email" className="form-label">Email</label>
                            <input required autoComplete='chrome-off' value={this.state.email} onChange={(e) => this.setState({ email: e.target.value })} type="email" className="form-control" id="email" placeholder="Digite o email" />
                        </div>
                        <div className="mb-3">
                            <label htmlFor="password" className="form-label">Senha</label>
                            <input required autoComplete='chrome-off' value={this.state.password} onChange={(e) => this.setState({ password: e.target.value })} type="password" className="form-control" id="password" placeholder="Digite a senha" />
                        </div>
                        <button type="submit" className="btn btn-primary">Criar</button>
                        {this.state.error && <p>{this.state.error}</p> }
                    </form>
                </div>
                <h1>Lista de Usuários</h1>
                <table className="table">
                    <thead>
                        <tr>
                            <th scope="col">ID</th>
                            <th scope="col">Nome</th>
                            <th scope="col">Deletar</th>
                        </tr>
                    </thead>
                    <tbody>
                        {userList.map((user, index) => (
                            <tr key={user.id}>
                                <td scope="row">{user.id}</td>
                                <td>{user.name}</td>
                                {userList.length <= 1 ? <td></td> : <td><button type="submit" className="btn btn-danger" onClick={(e) => this.deleteUser(user.id, e)}>Deletar</button></td>}
                            </tr>
                        ))}
                    </tbody>
                </table>
               
            </div>
        );
    }
}
