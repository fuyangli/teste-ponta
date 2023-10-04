import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = {
            user: JSON.parse(localStorage.getItem('user'))
        };
    }

    componentDidMount() {
        this.setState({ user: JSON.parse(localStorage.getItem('user')) });
    }

  render() {
      return (<>
          <h1>Bem vindo(a) {this.state.user?.name} | Ponta Agro</h1>
      </>
    );
  }
}
