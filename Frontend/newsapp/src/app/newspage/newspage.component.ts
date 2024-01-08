import { Component } from '@angular/core';
import { ExternaldataService } from '../externaldata.service';
import { WishserviceService } from '../wishservice.service';
import {  ElementRef, OnInit ,ViewChild} from '@angular/core';
import { Router } from '@angular/router';
import { LogService } from '../log.service';


@Component({
  selector: 'app-newspage',
  templateUrl: './newspage.component.html',
  styleUrls: ['./newspage.component.css']
})
export class NewspageComponent {
  news!: any;
  title = "Current News";
  userName = localStorage.getItem('userName');

  
  @ViewChild('searchInput', { static: false }) searchInput!: ElementRef;

  constructor(private newsservice:ExternaldataService,private wishlistService:WishserviceService,public router:Router,private logService:LogService){}
  
  

  ngOnInit():void{
   this.getNews("sports");
  }
  getNews(search :string):void{
    this.newsservice.getNews(search).subscribe(res=>{
      this.news=res||[];
      console.log(res);
      this.logService.log('Data is fetched');   
     
    })
  }

  onSearch() {
    const searchTerm = this.searchInput.nativeElement.value;
    this.getNews(searchTerm);
    this.logService.log('Search completed');
  }

  

  addToFavorites(FavouriteNews: any) {
    if(this.userName)
    {
     
        this.wishlistService.AddToWishlist(FavouriteNews,this.userName).subscribe(
          (response: any) => {
            this.news=response;
            // Handle successful login response
            console.log('News Added as a Favorite',response);
            this.logService.info("News Added in wishlist");
            this.router.navigate(['./wishlist']);
          },
          (error: any) => {
            // Handle login error
            console.error('Not Added failed', error);
          }
        );
    }
    else{
      this.router.navigate(['login']);
    }
  }

  logout(){
    this.logService.info("User logged out");
    localStorage.clear();
    sessionStorage.clear();
    this.router.navigate(['']);
  }
}
