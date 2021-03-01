import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpEventType
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { LoadingService } from 'src/app/services/loading.service';

@Injectable()
export class LoadingStatusInterceptor implements HttpInterceptor {

  constructor(private loadingService: LoadingService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.loadingService.isLoading.next(true);

    return next.handle(request).pipe(
      tap(resp => {
        if (resp.type == HttpEventType.Response)
          this.loadingService.isLoading.next(false);
      }),
      catchError(() => {
        this.loadingService.isLoading.next(false);
        return throwError('Ocorreu um erro');
      })
    );
  }
}
