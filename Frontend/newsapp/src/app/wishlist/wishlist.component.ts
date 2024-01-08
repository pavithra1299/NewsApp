import { Component, OnInit } from '@angular/core';
import { WishserviceService } from '../wishservice.service';
import { Router } from '@angular/router';
import { LogService } from '../log.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',  
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent implements OnInit {
  
  wish : any;
  userName = localStorage.getItem('userName');
  news!: any;

  constructor(private wishService:WishserviceService,public router:Router,private logService:LogService){}
  ngOnInit(): void {
   this.getWishlist(this.userName);
  }

  getWishlist(uname : any){
    this.wishService.GetWishlist(uname).subscribe(res=>{
      this.news=(res);
      this.logService.info("Fetched user wishlist");
      console.log(res);
    })

  }

  delete(author : any)
  {
    if(this.userName)
    {
     
        this.wishService.DeleteFromWishlist(this.userName,author).subscribe(
          (response: any) => {
            this.news=response;
            window.alert('News deleted');
            console.log('News Deleted from Favorite',response);
            this.logService.info("News deleted from wishlist");
            //this.router.navigate(['./wishlist']);
            this.getWishlist(this.userName);
            
            
          },
          (error: any) => {
            // Handle login error
            this.logService.error("News not deleted")
            console.error('Delete failed', error);
          }
        );
    }
   
  }

  logout(){
    this.logService.info("User logged Out");
    localStorage.clear();
    sessionStorage.clear();
    this.router.navigate(['']);
  }
  }

 


