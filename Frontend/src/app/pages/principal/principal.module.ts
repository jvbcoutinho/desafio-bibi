import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { PrincipalComponent } from './principal.component';
import { ListComponent } from './list/list.component';
import { UploadComponent } from './upload/upload.component';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon'
import { MatTooltipModule } from '@angular/material/tooltip';


const routes: Routes = [
  {
    path: '',
    component: PrincipalComponent,
    pathMatch: "full"
  }
];

@NgModule({
  declarations: [PrincipalComponent, UploadComponent, ListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,

    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatListModule,
    MatTableModule,
    MatIconModule,
    MatTooltipModule
  ]
})
export class PrincipalModule { }
