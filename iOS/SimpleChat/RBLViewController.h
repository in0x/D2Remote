//
//  RBLViewController.h
//  SimpleChat
//
//  Created by redbear on 14-4-8.
//  Copyright (c) 2014年 redbear. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <AVFoundation/AVFoundation.h>
#import "BLE.h"
#import "RBLAppDelegate.h"

BLE *bleShield; //Global because Objective C is the worst fucking language in the history off shitty languages

@protocol RBLVCdelegate
- (void) sendDataToArduino:(NSString*)data;
@end

@interface RBLViewController : UIViewController <RBLVCdelegate, UITableViewDataSource, UITableViewDelegate, UITextFieldDelegate, BLEDelegate>
{
    UIActivityIndicatorView *activityIndicator;
}

@property (nonatomic, weak) IBOutlet UITableView *tableView;
@property (nonatomic, weak) IBOutlet UITextField *text;

@end
