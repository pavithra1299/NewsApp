import { Component, OnInit } from '@angular/core';
import { ExternaldataService } from '../externaldata.service';
import { LogService } from '../log.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {
  title = "Current News";
  news!: any;

  constructor(private newsservice:ExternaldataService,private logService:LogService){}
  
  

  ngOnInit():void{
    this.newsservice.getNews("sports").subscribe(res=>{
      this.news=res;
      console.log(res);
      this.logService.log('Data is fetched from External API');
    })
  }

 
}
