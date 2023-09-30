import { Home } from "./components/Home";
import { Tasks } from "./components/Tasks";
import { Users } from "./components/Users";

const AppRoutes = [
    {
        index: true,
        path: '/',
        element: <Home />
    },
    {
        path: '/tasks',
        element: <Tasks />
    },
    {
        path: '/users',
        element: <Users />
    }
];

export default AppRoutes;
