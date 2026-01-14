import { RouterOutlet, Routes } from '@angular/router';
import { TaskList } from './core/components/task-list/task-list';
import { AddTask } from './core/components/add-task/add-task';
import { UpdateTask } from './core/components/update-task/update-task';

export const routes: Routes = [
  {
    path: 'tasks', // Parent Route For Task Management
    children: [
      { path: '', component: TaskList }, // Default Route To Show Task List
      { path: 'add', component: AddTask }, // Route To Add A New Task
      { path: 'edit/:id', component: UpdateTask }, // Route To Edit An Existing Task
    ],
  },
  { path: '', redirectTo: 'tasks', pathMatch: 'full' }, // Redirect Root To Tasks
];
