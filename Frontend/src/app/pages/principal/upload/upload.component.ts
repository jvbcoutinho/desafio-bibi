import { Component, OnInit } from '@angular/core';
import { ArquivoService } from 'src/app/services/arquivo.service';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

  public file: File = new File([], '');

  constructor(
    private arquivoService: ArquivoService
  ) { }

  ngOnInit(): void {
    // this.getAnalisysById();
  }

  handleFileInput(input: any){
    if(!input.files[0])
      this.file = new File([], '');
    else
      this.file = input.files[0];
  }

  uploadFile(){
    this.arquivoService.postFile(this.file).subscribe(result => {
      this.file = new File([], '');
    })
  }

}
