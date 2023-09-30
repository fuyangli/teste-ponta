import React, { Component, useState } from 'react';
import api from '.././api';

export class Users extends Component {
    
    constructor(props) {
        super(props);
        this.state = {
            userList: []
        };
    }

    static displayName = Users.name;

    componentDidMount() {
        api.get('/api/users', {})
            .then(x => {
                console.log(x);
                this.setState({userList: x.data})
            });
        
    }

    render() {
        const { userList } = this.state;
        return (
            <div>
                <h1>Lista de Usuários</h1>
                <ul>
                    {userList.map((user, index) => {
                        return <li key={user.id}>{user.name}</li>;
                    })}
                </ul>
            </div>
        );
    }
}
