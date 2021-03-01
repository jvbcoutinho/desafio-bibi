import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, pipe } from 'rxjs';
import { Arquivo } from '../shared/models/arquivo';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ArquivoService {

  $arquivos = new BehaviorSubject<Arquivo[]>([]);
  private baseUrl: string;

  constructor(
    private httpClient: HttpClient,
  ) {
    this.baseUrl = environment.serverUrl;
  }

  postFile(fileToUpload: File) {
    const endpoint = this.baseUrl + '/Arquivo';
    const formData: FormData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    return this.httpClient
      .post<Arquivo>(endpoint, formData)
      .pipe(
        tap(resp => {
          this.$arquivos.next([...this.$arquivos.value, resp]);
        })
      )
  }

  obterTodos() {
    const endpoint = this.baseUrl + `/Arquivo`;
    return this.httpClient.get<Arquivo[]>(endpoint).pipe(
      tap(resp => {
        this.$arquivos.next(resp);
      })
    )
  }

  excluir(id: string) {
    const endpoint = `https://localhost:5001/Arquivo/` + id;
    return this.httpClient.delete(endpoint).pipe(
      tap(() => {
        this.$arquivos.next(this.$arquivos.value.filter(x => x.resourceId != id));
      })
    );
  }
}
