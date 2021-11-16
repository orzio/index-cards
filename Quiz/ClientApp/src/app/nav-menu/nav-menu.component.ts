import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { AuthService } from '../services/auth-service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  isLoggedIn: boolean;
  constructor(private authService: AuthService, private router:Router) {

  }
  ngOnInit(): void {
    this.authService.currentUser.pipe(map(user => { this.isLoggedIn = user != null })).subscribe();
  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  logOut() {
    this.authService.logOut();
    this.router.navigateByUrl('/');
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
