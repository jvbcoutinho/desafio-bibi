import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToolbarComponent } from './toolbar/toolbar.component';

import { MatToolbarModule } from '@angular/material/toolbar';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoadingStatusInterceptor } from './interceptors/loading-status.interceptor';


@NgModule({
  declarations: [ToolbarComponent],
  imports: [
    CommonModule,

    MatToolbarModule,
  ],
  exports:
  [
    ToolbarComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: LoadingStatusInterceptor, multi: true },
  ]
})
export class CoreModule { }
