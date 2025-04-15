using EnvilopeChako.Authentication; 
using System;
using R3;
using UnityEngine;

public class TestRegisterView : IRegisterView
{
    private readonly Subject<Unit> onRegisterSubmitClicked = new();
    public Observable<Unit> OnRegisterSubmitClicked => onRegisterSubmitClicked;

    public string NicknameInput { get; set; }
    public string EmailInput { get; set; }
    public string PasswordInput { get; set; }
    public bool IsActive { get; private set; }
    public string Message { get; private set; }

    public void SimulateRegisterClick()
    {
        onRegisterSubmitClicked.OnNext(Unit.Default);
    }

    public void ShowMessage(string message)
    {
        Debug.Log(message);
        Message = message;
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }
}

public class TestVerificationView : IVerificationView
{
    private readonly Subject<Unit> onVerificationSubmitClicked = new Subject<Unit>();

    public Observable<Unit> OnVerificationSubmitClicked => onVerificationSubmitClicked;

    public string VerificationCodeInput { get; set; }
    public bool IsActive { get; private set; }
    public string Message { get; private set; }

    public void SimulateVerificationClick()
    {
        onVerificationSubmitClicked.OnNext(Unit.Default);
    }

    public void ShowMessage(string message)
    {
        Message = message;
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }
}

public class TestLoginView : ILoginView
{
    private readonly Subject<Unit> registerOpenSubject = new();
    public Observable<Unit> OnRegisterClicked => registerOpenSubject;
    public string EmailInput { get; set; }
    public string PasswordInput { get; set; }
    public bool IsActive { get; private set; }
    public string Message { get; private set; }
    
    private readonly Subject<Unit> onLoginSubmitClicked = new();
    public Observable<Unit> OnLoginSubmitClicked => onLoginSubmitClicked;
    
    public void SimulateLoginClick()
    {
        onLoginSubmitClicked.OnNext(Unit.Default);
    }
    
    public void SimulateRegisterClick()
    {
        registerOpenSubject.OnNext(Unit.Default);
    }
    
    public void ShowMessage(string message)
    {
        Message = message;
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }
}