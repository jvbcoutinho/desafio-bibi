import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ArquivoService } from 'src/app/services/arquivo.service';
import { Arquivo } from 'src/app/shared/models/arquivo';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  displayedColumns: string[] = ['status', 'nome', 'delete'];

  dataSource = new MatTableDataSource<Arquivo>();

  constructor(private arquivoService: ArquivoService) { }

  ngOnInit(): void {
    this.arquivoService.obterTodos().subscribe();
    this.arquivoService.$arquivos.subscribe(arqs =>
      this.dataSource.data = arqs
    );
  }

  atualizarStatus() {
    this.arquivoService.obterTodos().subscribe();
  }

  excluir(arq: Arquivo) {
    this.arquivoService.excluir(arq.resourceId).subscribe();
  }

}
