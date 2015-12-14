//
//  RBLAppDelegate.h
//  SimpleChat
//
//  Created by redbear on 14-4-8.
//  Copyright (c) 2014年 redbear. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "RBLViewController.h"
#import "BLE.h"

@protocol RBLVCdelegate;

@interface RBLAppDelegate : UIResponder <UIApplicationDelegate, UIAlertViewDelegate> {
    id <RBLVCdelegate> __unsafe_unretained delegate;
    BLE* shield;
}

@property (unsafe_unretained) id<RBLVCdelegate> delegate;

@property (strong, nonatomic) UIWindow *window;
//@property RBLViewController* viewController;

@end
