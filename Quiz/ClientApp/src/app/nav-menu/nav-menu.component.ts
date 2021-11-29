import { OnDestroy } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthService } from '../services/auth-service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit, OnDestroy {

  isLoggedIn: boolean;
  private subscriptions = new Subscription();
  constructor(private authService: AuthService, private router:Router) {

  }

  ngOnInit(): void {
    let sub = this.authService.currentUser.pipe(map(user => { this.isLoggedIn = user != null })).subscribe();
    this.subscriptions.add(sub);
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

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

}
